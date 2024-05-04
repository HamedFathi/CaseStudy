namespace CaseStudy.Application.ContactPersonCQ;

public class ContactPersonDTO
{
    public int VendorId { get; set; }
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Phone { get; set; } = null!;
}