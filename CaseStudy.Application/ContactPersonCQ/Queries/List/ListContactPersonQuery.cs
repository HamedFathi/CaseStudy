using HamedStack.CQRS;

namespace CaseStudy.Application.ContactPersonCQ.Queries.List;

public class ListContactPersonQuery : IQuery<IEnumerable<ContactPersonDTO>>
{
    public int VendorId { get; set; }
    public int? PageIndex { get; set; }
    public int? PageSize { get; set; }
}