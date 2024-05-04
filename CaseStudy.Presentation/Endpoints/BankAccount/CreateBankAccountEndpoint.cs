using CaseStudy.Application.BankAccountCQ.Commands.Create;
using CaseStudy.Infrastructure.Identity.Permissions;
using HamedStack.AspNetCore.Endpoint;
using HamedStack.CQRS;
using HamedStack.TheResult;
using Microsoft.AspNetCore.Authorization;

namespace CaseStudy.Presentation.Endpoints.BankAccount;

public class CreateBankAccountEndpoint : IMinimalApiEndpoint
{
    public void HandleEndpoint(IEndpointRouteBuilder endpoint)
    {
        endpoint.MapPost("/bank", CreateBankAccountEndpointHandler);
    }

    [Authorize]
    [PermissionAuthorize(Permission.Create)]
    private static async Task<Result<int>> CreateBankAccountEndpointHandler(CreateBankAccountCommand createBankAccountCommand, ICommandQueryDispatcher dispatcher)
    {
        var output = await dispatcher.Send(createBankAccountCommand);
        return output;
    }
}