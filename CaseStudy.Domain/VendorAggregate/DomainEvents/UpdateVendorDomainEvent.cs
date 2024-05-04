using HamedStack.TheAggregateRoot.Events;

namespace CaseStudy.Domain.VendorAggregate.DomainEvents;

public class UpdateVendorDomainEvent : DomainEvent
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Mail { get; set; } = null!;

}