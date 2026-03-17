using Ardalis.Specification;

namespace Bsol.Business.Template.Core.AccountAggregate.Specifications;

public class AccountByIdAccountSpec : Specification<Account>, ISingleResultSpecification<Account>
{
    public AccountByIdAccountSpec(Guid? accountId, string? accountNumber)
    {
        Query.Where(Account => Account.Id == accountId || Account.AccountNumber == accountNumber);
    }
}
