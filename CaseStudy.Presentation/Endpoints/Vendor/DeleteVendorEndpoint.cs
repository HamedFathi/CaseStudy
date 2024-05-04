using CaseStudy.Application.VendorCQ.Commands.Delete;
using CaseStudy.Infrastructure.Identity.Permissions;
using HamedStack.AspNetCore.Endpoint;
using HamedStack.CQRS;
using HamedStack.TheResult;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CaseStudy.Presentation.Endpoints.Vendor;

public class DeleteVendorEndpoint : IMinimalApiEndpoint
{
    public void HandleEndpoint(IEndpointRouteBuilder endpoint)
    {
        endpoint.MapDelete("/vendor/{id:int}", DeleteVendorEndpointHandler);
    }

    [Authorize]
    [PermissionAuthorize(Permission.Delete)]
    private static async Task<Result> DeleteVendorEndpointHandler([FromRoute] int id, ICommandQueryDispatcher dispatcher)
    {
        await dispatcher.Send(new DeleteVendorCommand() { Id = id });
        return Result.Success();
    }
}