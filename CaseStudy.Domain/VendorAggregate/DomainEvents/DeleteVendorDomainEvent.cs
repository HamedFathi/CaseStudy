using HamedStack.TheAggregateRoot.Events;

namespace CaseStudy.Domain.VendorAggregate.DomainEvents;

public class DeleteVendorDomainEvent : DomainEvent
{
    public int Id { get; set; }
}