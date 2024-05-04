using HamedStack.CQRS;

namespace CaseStudy.Application.VendorCQ.Queries.Get;

public class GetVendorByIdQuery : IQuery<VendorDTO>
{
    public int Id { get; set; }
}