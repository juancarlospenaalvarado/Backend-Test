using Ardalis.Result;
using Ardalis.SharedKernel;
using Bsol.Business.Template.Core.UseCases.Transaction.Common;

namespace Bsol.Business.Template.Core.UseCases.Transaction.GetTransaction;

public record GetTransactionQuery() : ICommand<Result<List<TransactionDto>>>
{
}
