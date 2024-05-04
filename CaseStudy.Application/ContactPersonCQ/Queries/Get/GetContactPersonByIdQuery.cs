using HamedStack.CQRS;

namespace CaseStudy.Application.ContactPersonCQ.Queries.Get;

public class GetContactPersonByIdQuery : IQuery<ContactPersonDTO>
{
    public int VendorId { get; set; }
    public int Id { get; set; }
}