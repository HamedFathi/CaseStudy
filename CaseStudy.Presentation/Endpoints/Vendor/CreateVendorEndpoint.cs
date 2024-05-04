using CaseStudy.Application.VendorCQ.Commands.Create;
using CaseStudy.Infrastructure.Identity.Permissions;
using HamedStack.AspNetCore.Endpoint;
using HamedStack.CQRS;
using HamedStack.TheResult;
using Microsoft.AspNetCore.Authorization;

namespace CaseStudy.Presentation.Endpoints.Vendor;

public class CreateVendorEndpoint : IMinimalApiEndpoint
{
    public void HandleEndpoint(IEndpointRouteBuilder endpoint)
    {
        endpoint.MapPost("/vendor", CreateVendorEndpointHandler);
    }

    [Authorize]
    [PermissionAuthorize(Permission.Create)]
    private static async Task<Result<int>> CreateVendorEndpointHandler(CreateVendorCommand createVendorCommand, ICommandQueryDispatcher dispatcher)
    {
        var output = await dispatcher.Send(createVendorCommand);
        return output;
    }
}