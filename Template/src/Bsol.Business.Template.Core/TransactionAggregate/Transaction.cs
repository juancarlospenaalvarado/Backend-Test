using Ardalis.GuardClauses;
using Bsol.Business.Template.SharedKernel.Audit;
using Bsol.Business.Template.SharedKernel.Interfaces;

namespace Bsol.Business.Template.Core.TransactionAggregate;

public class Transaction(string sourceAccountId,string destinationAccountId,string voucherCode, decimal amount) 
    : AuditableEntity, IAggregateRoot
{
    public string SourceAccountId { get; set; } = Guard.Against.NullOrEmpty(sourceAccountId);
    public string DestinationAccountId { get; set; } = Guard.Against.NullOrEmpty(destinationAccountId);
    public string VoucherCode { get; set; } = Guard.Against.NullOrEmpty(voucherCode);
    public decimal Amount { get; set; } = Guard.Against.NegativeOrZero(amount);

    public Transaction(Guid id, string sourceAccountId, string destinationAccountId, string voucherCode, decimal amount) 
        : this(sourceAccountId, destinationAccountId, voucherCode, amount)
    {
        SourceAccountId = sourceAccountId;
        DestinationAccountId = destinationAccountId;
        VoucherCode = voucherCode;
        Amount = amount;
        Id = id;
    }
}
