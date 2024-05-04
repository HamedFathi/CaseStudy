using CaseStudy.Domain.VendorAggregate.ValueObjects;
using HamedStack.TheAggregateRoot;

namespace CaseStudy.Domain.VendorAggregate.Entities;

public class ContactPerson : Entity<int>
{
    public Name FirstName { get; set; } = null!;
    public Name LastName { get; set; } = null!;
    public Email Email { get; set; } = null!;
    public Phone Phone { get; set; } = null!;
    public virtual Vendor Vendor { get; set; } = null!;

}