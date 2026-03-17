using Ardalis.Result;
using Ardalis.SharedKernel;
using Bsol.Business.Template.Core.UseCases.Transaction.Common;
using static System.Net.Mime.MediaTypeNames;

namespace Bsol.Business.Template.Core.UseCases.Transaction.GetTransaction;

internal class GetTransactionQueryHandler(SharedKernel.Interfaces.IRepository<TransactionAggregate.Transaction> _repository)
    : ICommandHandler<GetTransactionQuery, Result<List<TransactionDto>>>
{
    public async Task<Result<List<TransactionDto>>> Handle(GetTransactionQuery request, CancellationToken cancellationToken)
    {
        try
        {


            var transactions = await _repository.ListAsync( cancellationToken);

            var TransactionDto = transactions.Select(x => new TransactionDto(x)).ToList();


            return TransactionDto;
        }
        catch (Exception ex)
        {

            return Result.Error(ex.Message);
        }
    }
}
