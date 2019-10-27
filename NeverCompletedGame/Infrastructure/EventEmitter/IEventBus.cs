using System;
using System.Collections.Generic;
using System.Reactive;
using Infrastructure.Events;

namespace Infrastructure.EventEmitter
{
    public interface IEventBus
    {
        IObservable<EventPattern<IEventSourcingEvent>> Events { get; }

        void Emit(IEnumerable<IEventSourcingEvent> @events);

        void Emit(IEventSourcingEvent @event);
    }
}
