using Bsol.Business.Template.Api.Endpoints.ManagementBankTransfer;
using Bsol.Business.Template.Core.UseCases.Transaction.GetTransaction;
using FastEndpoints;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Bsol.Business.Template.Api.Endpoints.Account;

public class GetAccount(IMediator _mediator) : EndpointWithoutRequest<Results<Ok<List<GetTransactionResponse>>, NotFound, ProblemDetails>>
{

    public override void Configure()
    {
        Version(1);
        Get("/GetTransaction");
        AllowAnonymous();
    }

    public override async Task<Results<Ok<List<GetTransactionResponse>>, NotFound, ProblemDetails>> ExecuteAsync(CancellationToken ct)
    {


        var result = await _mediator.Send(new GetTransactionQuery(), ct);

        if (!result.IsSuccess)
        {

            return new ProblemDetails
            {
                Detail = result.Errors.FirstOrDefault(),
                Status = StatusCodes.Status403Forbidden
            };
        }
        var response = result.Value.Select(x =>
        new GetTransactionResponse(x.Id, x.VoucherCode, x.SourceAccountId, x.DestinationAccountId, x.Amount)).ToList();
        return TypedResults.Ok(response);

    }

}
