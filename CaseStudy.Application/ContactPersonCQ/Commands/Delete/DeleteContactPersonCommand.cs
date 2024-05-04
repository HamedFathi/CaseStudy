using HamedStack.CQRS;

namespace CaseStudy.Application.ContactPersonCQ.Commands.Delete;

public class DeleteContactPersonCommand : ICommand
{
    public int Id { get; set; }
    public int VendorId { get; set; }
}