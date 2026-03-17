using Ardalis.Result;
using Ardalis.SharedKernel;
using Bsol.Business.Template.Core.AccountAggregate.Specifications;
using Bsol.Business.Template.Core.UseCases.Account.Dto;


namespace Bsol.Business.Template.Core.UseCases.Account.GetAccount;

public class GetAccountQueryHandler(SharedKernel.Interfaces.IRepository<AccountAggregate.Account> _repository) 
    : ICommandHandler<GetAccountQuery, Result<AccountDto>>
{
    public async Task<Result<AccountDto>> Handle(GetAccountQuery request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.accountId == null && request.accountNumber == null)
            {
                return Result.Error("No se puede realizar la busqueda sin algun parametro solicitado");
            }
            var spec = new AccountByIdAccountSpec(request.accountId , request.accountNumber );
            
            var account = await _repository.FirstOrDefaultAsync(spec, cancellationToken);

            var accountDto = new AccountDto(account);
    
            return accountDto;
        }
        catch (Exception ex)
        {

            return Result.Error(ex.Message);
        }
    }
}
