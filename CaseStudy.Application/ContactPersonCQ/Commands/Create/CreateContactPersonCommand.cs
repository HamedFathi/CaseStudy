using HamedStack.CQRS;

namespace CaseStudy.Application.ContactPersonCQ.Commands.Create;

public class CreateContactPersonCommand : ICommand<int>
{
    public int VendorId { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Phone { get; set; } = null!;
}