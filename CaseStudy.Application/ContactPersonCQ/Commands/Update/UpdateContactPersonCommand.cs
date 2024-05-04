using HamedStack.CQRS;

namespace CaseStudy.Application.ContactPersonCQ.Commands.Update;

public class UpdateContactPersonCommand : ICommand
{
    public int VendorId { get; set; }
    public int Id { get; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Phone { get; set; } = null!;
}