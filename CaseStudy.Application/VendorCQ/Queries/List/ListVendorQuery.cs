using HamedStack.CQRS;

namespace CaseStudy.Application.VendorCQ.Queries.List;

public class ListVendorQuery : IQuery<IEnumerable<VendorDTO>>
{
    public int? PageIndex { get; set; }
    public int? PageSize { get; set; }
}