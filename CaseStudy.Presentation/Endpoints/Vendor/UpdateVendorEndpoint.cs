using CaseStudy.Application.VendorCQ.Commands.Update;
using CaseStudy.Infrastructure.Identity.Permissions;
using HamedStack.AspNetCore.Endpoint;
using HamedStack.CQRS;
using HamedStack.TheResult;
using Microsoft.AspNetCore.Authorization;

namespace CaseStudy.Presentation.Endpoints.Vendor;

public class UpdateVendorEndpoint : IMinimalApiEndpoint
{
    public void HandleEndpoint(IEndpointRouteBuilder endpoint)
    {
        endpoint.MapPut("/vendor", UpdateVendorEndpointHandler);
    }

    [Authorize]
    [PermissionAuthorize(Permission.Create)]
    private static async Task<Result> UpdateVendorEndpointHandler(UpdateVendorCommand updateVendorCommand, ICommandQueryDispatcher dispatcher)
    {
        var output = await dispatcher.Send(updateVendorCommand);
        return output;
    }
}