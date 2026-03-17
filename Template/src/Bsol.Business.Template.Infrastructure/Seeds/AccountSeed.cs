namespace Bsol.Business.Template.Infrastructure.Seeds;

public static class AccountSeed
{
    public static List<Core.AccountAggregate.Account> SeedAccount()
    {
        return [
            new Core.AccountAggregate.Account(Guid.NewGuid(), "123456", 10),
            new Core.AccountAggregate.Account(Guid.NewGuid(), "123457", 10) ,
            new Core.AccountAggregate.Account(Guid.NewGuid(), "123458", 10)
        ];

    }
}

