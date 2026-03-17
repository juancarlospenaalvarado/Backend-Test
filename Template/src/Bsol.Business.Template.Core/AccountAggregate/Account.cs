using Ardalis.GuardClauses;
using Bsol.Business.Template.SharedKernel.Audit;
using Bsol.Business.Template.SharedKernel.Interfaces;

namespace Bsol.Business.Template.Core.AccountAggregate;

public class Account(string accountNumber,decimal balance) : AuditableEntity, IAggregateRoot
{
    public string AccountNumber { get; set; } = Guard.Against.NullOrEmpty(accountNumber);
    public decimal Balance { get; set; } = Guard.Against.Negative(balance);

    public Account(Guid id, string accountNumber,decimal balance ) : this(accountNumber , balance)
    {
        AccountNumber = accountNumber;
        Balance = balance;
        Id = id;
    }
}
