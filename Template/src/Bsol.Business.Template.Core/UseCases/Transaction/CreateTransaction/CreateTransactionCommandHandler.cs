using Ardalis.Result;
using Ardalis.SharedKernel;
using Bsol.Business.Template.Core.AccountAggregate.Specifications;
using Bsol.Business.Template.Core.Interfaces.Services;
using Bsol.Business.Template.Core.UseCases.Transaction.Common;

namespace Bsol.Business.Template.Core.UseCases.Transaction.CreateTransaction;

public class CreateTransactionCommandHandler(SharedKernel.Interfaces.IRepository<TransactionAggregate.Transaction> _repository,
    
    SharedKernel.Interfaces.IRepository<AccountAggregate.Account> _repositoryAccount) : ICommandHandler<CreateTransactionCommand, Result<TransactionDto>>
{
    public async Task<Result<TransactionDto>> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.SourceAccountNumber == request.DestinationAccountNumber)
            {
                return Result.Error("No se puede debitar a la misma cuenta");
            }

            var specAccountSource = new AccountByAccounNumberSpec(request.SourceAccountNumber);
            var accountSource = await _repositoryAccount.FirstOrDefaultAsync(specAccountSource, cancellationToken);
            if (accountSource == null)
            {
                return Result.Error("No se puede encontrar la cuenta de origen");
            }
            if (accountSource.Balance < request.Amount)
            {
                return Result.Error("No se puede realizar la operacion porque es mayor que el monto disponible");
            }

            var specAccountDestination = new AccountByAccounNumberSpec(request.DestinationAccountNumber);
            var accountDestination = await _repositoryAccount.FirstOrDefaultAsync(specAccountDestination, cancellationToken);
            if (accountDestination == null)
            {
                return Result.Error("No se puede encontrar la cuenta de Destino");
            }

            accountSource.Balance -= request.Amount;
            accountDestination.Balance += request.Amount;

            await _repositoryAccount.UpdateAsync(accountSource);
            await _repositoryAccount.UpdateAsync(accountDestination);
            var newVoucher = Guid.NewGuid();

            var Transaction = new TransactionAggregate.Transaction(request.SourceAccountNumber,request.DestinationAccountNumber, newVoucher.ToString(), request.Amount);
            var resultCreate = await _repository.AddAsync(Transaction, cancellationToken);

            if (resultCreate == null) return Result.Error("No se puedo registrar la transaccion.");

            var TransactionDto = new TransactionDto(resultCreate);

            return TransactionDto;
        }
        catch (Exception ex)
        {

            return Result.Error(ex.Message);
        }
    }
}
