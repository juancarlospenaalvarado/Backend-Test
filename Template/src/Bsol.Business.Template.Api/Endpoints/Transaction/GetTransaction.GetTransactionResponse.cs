namespace Bsol.Business.Template.Api.Endpoints.ManagementBankTransfer;

public record GetTransactionResponse(Guid Id, string voucherCode, string SourceAccountId, string DestinationAccountId, decimal Amount);
