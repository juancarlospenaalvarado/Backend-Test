using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Bsol.Business.Template.Core.Interfaces.Services;
using Bsol.Business.Template.Infrastructure.Data;
using Bsol.Business.Template.Infrastructure.Seeds;
using Bsol.Business.Template.Infrastructure.Services;
using Bsol.Business.Template.SharedKernel;
using Bsol.Business.Template.SharedKernel.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;



namespace Bsol.Business.Template.Infrastructure;

[ExcludeFromCodeCoverage]
public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration, Assembly? callingAssembly = null)
    {
        // Use for Specification Repository
        services.AddTransient(typeof(IRepository<>), typeof(EfRepository<>));
        services.AddTransient(typeof(IReadRepository<>), typeof(EfRepository<>));
        // Use for Domaint Events
        services.AddTransient<IMediator, Mediator>();
        services.AddTransient<IDomainEventDispatcher, DomainEventDispatcher>();
        services.AddMediatR(config => config.RegisterServicesFromAssemblies(GetAssemblies(callingAssembly)));


        services.AddMemoryCache();
        services.AddScoped(typeof(ICommandBaseRepository<>), typeof(CommandRepository<>));

        var pokeApiBaseUrl = configuration.GetValue<string>($"{nameof(PokeApiService)}:BaseUrl");
        services.AddPokeApi(pokeApiBaseUrl);
        return services;
    }

    private static Assembly[] GetAssemblies(Assembly? callingAssembly)
    {
        var _assemblies = new List<Assembly>();
        var coreAssembly =
          Assembly.GetAssembly(Bsol.Business.Template.Core.ConfigureServices.CoreAssembly.GetType());
        var infrastructureAssembly = Assembly.GetAssembly(typeof(AppDbContext));
        if (coreAssembly != null)
        {
            _assemblies.Add(coreAssembly);
        }

        if (infrastructureAssembly != null)
        {
            _assemblies.Add(infrastructureAssembly);
        }

        if (callingAssembly != null)
        {
            _assemblies.Add(callingAssembly);
        }
        return [.. _assemblies];
    }
    public static IServiceCollection AddPokeApi(this IServiceCollection services, string? baseUrl)
    {
        if (string.IsNullOrEmpty(baseUrl))
        {
            return services;
        }
        services.AddTransient<IPokeApiService, PokeApiService>();
        services.AddHttpClient<PokeApiService>(client =>
        {
            client.BaseAddress = new Uri(baseUrl);
        });
        return services;
    }

}
