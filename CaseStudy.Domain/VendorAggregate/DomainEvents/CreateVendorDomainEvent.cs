using HamedStack.TheAggregateRoot.Events;

namespace CaseStudy.Domain.VendorAggregate.DomainEvents;

public class CreateVendorDomainEvent : DomainEvent
{
    public string Name { get; set; } = null!;
    public string Mail { get; set; } = null!;
}