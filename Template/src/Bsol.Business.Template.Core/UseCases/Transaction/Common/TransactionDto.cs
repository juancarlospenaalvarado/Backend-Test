namespace Bsol.Business.Template.Core.UseCases.Transaction.Common;

public class TransactionDto
{
    public TransactionDto(TransactionAggregate.Transaction transaction)
    {
        Id = transaction.Id;
        VoucherCode = transaction.VoucherCode;
        SourceAccountId = transaction.SourceAccountId;
        DestinationAccountId = transaction.DestinationAccountId;
        Amount = transaction.Amount;
    }
    public Guid Id { get; set; }
    public string VoucherCode { get; set; }
    public string SourceAccountId { get; set; }
    public string DestinationAccountId { get; set; }
    public decimal Amount { get; set; }


}
