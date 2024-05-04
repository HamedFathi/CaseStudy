using CaseStudy.Application.VendorCQ;
using CaseStudy.Application.VendorCQ.Queries.List;
using CaseStudy.Infrastructure.Identity.Permissions;
using HamedStack.AspNetCore.Endpoint;
using HamedStack.CQRS;
using HamedStack.TheResult;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CaseStudy.Presentation.Endpoints.Vendor;

public class ListVendorEndpoint : IMinimalApiEndpoint
{
    public void HandleEndpoint(IEndpointRouteBuilder endpoint)
    {
        endpoint.MapGet("/vendor/list", ListVendorEndpointHandler);
    }
    [Authorize]
    [PermissionAuthorize(Permission.Read)]
    private static async Task<Result<IEnumerable<VendorDTO>>> ListVendorEndpointHandler([FromBody] ListVendorQuery listVendorQuery, ICommandQueryDispatcher dispatcher)
    {
        var output = await dispatcher.Send(listVendorQuery);
        return output;
    }
}