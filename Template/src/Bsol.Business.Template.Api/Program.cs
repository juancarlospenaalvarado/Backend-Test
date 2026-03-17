
using Bsol.Business.Template.Api;
using Bsol.Business.Template.Api.Extensions;
using Bsol.Business.Template.Core;
using Bsol.Business.Template.Infrastructure;
using Bsol.Business.Template.Infrastructure.Data;
using Bsol.Business.Template.Infrastructure.Seeds;
using Destructurama;
using FastEndpoints;
using FastEndpoints.Swagger;
using HealthChecks.UI.Client;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();
builder.Services.AddFastEndpoints();
builder.Services.AddCustomApiVersioning();
builder.Services.SwaggerDocument();
builder.Services.AddCors();
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? string.Empty;
builder.Services.AddPostgresDbContext(connectionString);
builder.Services.AddApiServices(builder.Configuration);
builder.Services.AddCoreServices();
builder.Services.AddInfrastructureServices(builder.Configuration);


if (!builder.Environment.IsProduction())
{
    string[] origins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>() ?? Array.Empty<string>();
    builder.Services.AddCors(o => o.AddPolicy("AllowedOrigins", builder =>
        builder.WithOrigins(origins)
               .AllowAnyHeader()
               .AllowAnyMethod()
    ));
}

var app = builder.Build();
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(app.Configuration)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.ApplicationInsights(app.Services.GetRequiredService<TelemetryConfiguration>(), TelemetryConverter.Traces)
    .Destructure.UsingAttributes()
    .CreateLogger();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    // Asegura que la DB exista
    await context.Database.MigrateAsync();

    // Evitar duplicados
    if (!context.Account.Any())
    {
        var accounts = AccountSeed.SeedAccount();

        await context.Account.AddRangeAsync(accounts);
        await context.SaveChangesAsync();
    }
}

app.UseFastEndpoints(c =>
{
    c.Versioning.Prefix = "Bsol/v";
    c.Versioning.PrependToRoute = true;
});
app.UseSwaggerGen(); // FastEndpoints middleware

// Exception Middleware 
app.UseHealthChecks("/health",
    new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
    {
        Predicate = _ => true,
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });

await app.RunAsync();

public partial class Program
{
    protected Program() { }
}
