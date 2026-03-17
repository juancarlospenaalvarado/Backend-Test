using Ardalis.Result;
using Ardalis.SharedKernel;
using Bsol.Business.Template.Core.UseCases.Transaction.Common;

namespace Bsol.Business.Template.Core.UseCases.Transaction.CreateTransaction;

public record CreateTransactionCommand(string SourceAccountNumber, string DestinationAccountNumber, decimal Amount) : ICommand<Result<TransactionDto>>
{
}
