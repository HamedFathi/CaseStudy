using CaseStudy.Application.VendorCQ.Queries.Get;
using CaseStudy.Infrastructure.Identity.Permissions;
using HamedStack.AspNetCore.Endpoint;
using HamedStack.CQRS;
using HamedStack.TheResult;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CaseStudy.Presentation.Endpoints.Vendor;

public class GetByIdVendorEndpoint : IMinimalApiEndpoint
{
    public void HandleEndpoint(IEndpointRouteBuilder endpoint)
    {
        endpoint.MapGet("/vendor/{id:int}", DeleteVendorEndpointHandler);
    }

    [Authorize]
    [PermissionAuthorize(Permission.Read)]
    private static async Task<Result> DeleteVendorEndpointHandler([FromRoute] int id, ICommandQueryDispatcher dispatcher)
    {
        var output = await dispatcher.Send(new GetVendorByIdQuery() { Id = id });
        return output;
    }
}