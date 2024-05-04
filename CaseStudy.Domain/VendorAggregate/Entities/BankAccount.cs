// ReSharper disable InconsistentNaming

using CaseStudy.Domain.VendorAggregate.ValueObjects;
using HamedStack.TheAggregateRoot;

namespace CaseStudy.Domain.VendorAggregate.Entities;

public class BankAccount : Entity<int>
{
    public Name Name { get; set; } = null!;
    public IBAN IBAN { get; set; } = null!;
    public BIC BIC { get; set; } = null!;
    public virtual Vendor Vendor { get; set; } = null!;

}