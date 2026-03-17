using System.ComponentModel.DataAnnotations;

namespace Bsol.Business.Template.Api.Endpoints.Account;

public class GetAccountRequest
{
    public const string Route = "/GetAccount";

    
    public Guid? AccountId { get; set; }
    public string? AccountNumber { get; set; }
}
