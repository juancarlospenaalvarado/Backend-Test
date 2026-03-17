using System.ComponentModel.DataAnnotations;

namespace Bsol.Business.Template.Api.Endpoints.Template;

public class CreateTransactionRequest
{
    public const string Route = "/CreateTransaction";

    [Required]
    public required string SourceAccountNumber { get; set; }
    public required string DestinationAccountNumber { get; set; }
    public required decimal Amount { get; set; }
}
