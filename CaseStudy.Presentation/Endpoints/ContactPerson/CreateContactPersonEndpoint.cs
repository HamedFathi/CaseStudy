using CaseStudy.Application.ContactPersonCQ.Commands.Create;
using CaseStudy.Infrastructure.Identity.Permissions;
using HamedStack.AspNetCore.Endpoint;
using HamedStack.CQRS;
using HamedStack.TheResult;
using Microsoft.AspNetCore.Authorization;

namespace CaseStudy.Presentation.Endpoints.ContactPerson;

public class CreateContactPersonEndpoint : IMinimalApiEndpoint
{
    public void HandleEndpoint(IEndpointRouteBuilder endpoint)
    {
        endpoint.MapPost("/contact", CreateContactPersonEndpointHandler);
    }

    [Authorize]
    [PermissionAuthorize(Permission.Create)]
    private static async Task<Result<int>> CreateContactPersonEndpointHandler(CreateContactPersonCommand createBankAccountCommand, ICommandQueryDispatcher dispatcher)
    {
        var output = await dispatcher.Send(createBankAccountCommand);
        return output;
    }
}