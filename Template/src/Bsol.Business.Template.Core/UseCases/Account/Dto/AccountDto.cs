namespace Bsol.Business.Template.Core.UseCases.Account.Dto;

public class AccountDto
{
    public AccountDto(AccountAggregate.Account account) 
    { 
        Id = account.Id;
        AccountNumber = account.AccountNumber;
        Balance = account.Balance;
    }
    public Guid Id { get; set; }
    public string AccountNumber { get; set; }
    public decimal Balance { get; set; }
}
