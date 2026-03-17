using Bsol.Business.Template.Api.Endpoints.Account;
using Bsol.Business.Template.Core.UseCases.Account.GetAccount;
using FastEndpoints;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Bsol.Business.Template.Api.Endpoints.ManagementBankTransfer;

public class GetAccount(IMediator _mediator) : Endpoint<GetAccountRequest, Results<Ok<GetAccountResponse>, NotFound, ProblemDetails>>
{

    public override void Configure()
    {
        Version(1);
        Get(GetAccountRequest.Route);
        AllowAnonymous();
    }

    public override async Task<Results<Ok<GetAccountResponse>, NotFound, ProblemDetails>> ExecuteAsync(GetAccountRequest request, CancellationToken ct)
    {


        var result = await _mediator.Send(new GetAccountQuery(request.AccountId, request.AccountNumber), ct);

        if (!result.IsSuccess)
        {

            return new ProblemDetails
            {
                Detail = result.Errors.FirstOrDefault(),
                Status = StatusCodes.Status403Forbidden
            };
        }
        return TypedResults.Ok(new GetAccountResponse(result.Value.Id, result.Value.AccountNumber, result.Value.Balance));

    }

}
