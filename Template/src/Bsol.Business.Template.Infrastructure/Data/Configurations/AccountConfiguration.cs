using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bsol.Business.Template.Infrastructure.Data.Configurations;

public class AccountConfiguration : IEntityTypeConfiguration<Core.AccountAggregate.Account>
{
    public void Configure(EntityTypeBuilder<Core.AccountAggregate.Account> builder)
    {
        builder.ToTable("Account");
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id).IsRequired(true);
        builder.Property(a => a.AccountNumber).HasMaxLength(50).IsRequired(true);
        builder.Property(a => a.Balance).IsRequired(true);

    }
}
