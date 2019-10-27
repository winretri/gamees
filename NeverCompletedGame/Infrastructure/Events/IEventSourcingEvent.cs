using System;
using Playing.Events;

namespace Infrastructure.Events
{
    public interface IEventSourcingEvent
    {
        string AggregateId { get; }
        string DomainEventName { get; set; }
        string EventContainer { get; set; }
        IEvent DomainEvent { get; }
        int EventContainerVersion { get; }
        string Id { get; }
        int Sequence { get; }
        DateTime Time { get; }
    }
}