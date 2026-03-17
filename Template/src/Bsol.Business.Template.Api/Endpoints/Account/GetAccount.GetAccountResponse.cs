namespace Bsol.Business.Template.Api.Endpoints.Account;

public record GetAccountResponse(
    Guid Id,
    string AccountNumber,
    decimal Balance);

