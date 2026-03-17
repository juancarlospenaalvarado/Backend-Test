using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bsol.Business.Template.Infrastructure.Data.Configurations;

public class TransactionConfiguration : IEntityTypeConfiguration<Core.TransactionAggregate.Transaction>
{
    public void Configure(EntityTypeBuilder<Core.TransactionAggregate.Transaction> builder)
    {
        builder.ToTable("Transaction");
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id).IsRequired(true);
        builder.Property(a => a.SourceAccountId).HasMaxLength(50).IsRequired(true);
        builder.Property(a => a.DestinationAccountId).HasMaxLength(50).IsRequired(true);
        builder.Property(a => a.VoucherCode).HasMaxLength(50).IsRequired(true);
        builder.Property(a => a.Amount).IsRequired(true);

    }
}
