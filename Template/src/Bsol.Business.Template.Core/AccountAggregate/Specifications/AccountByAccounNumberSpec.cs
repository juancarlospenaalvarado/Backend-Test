using Ardalis.Specification;

namespace Bsol.Business.Template.Core.AccountAggregate.Specifications;

public class AccountByAccounNumberSpec : Specification<Account>, ISingleResultSpecification<Account>
{
    public AccountByAccounNumberSpec( string accountNumber)
    {
        Query.Where(Account => Account.AccountNumber == accountNumber);
    }
}
