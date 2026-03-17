using Ardalis.Result;
using Ardalis.SharedKernel;
using Bsol.Business.Template.Core.UseCases.Account.Dto;


namespace Bsol.Business.Template.Core.UseCases.Account.GetAccount;

public record GetAccountQuery(Guid? accountId, string? accountNumber) : ICommand<Result<AccountDto>>
{
}
