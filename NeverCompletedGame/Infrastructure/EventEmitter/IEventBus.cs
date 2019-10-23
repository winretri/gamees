using System;
using System.Collections.Generic;
using System.Reactive;
using System.Text;
using Playing;
using Playing.Events;

namespace Infrastructure.EventEmitter
{
    public interface IEventBus
    {
        IObservable<EventPattern<IEvent>> Events { get; }

        void Emit(IEnumerable<IEvent> @events);

        void Emit(IEvent @event);
    }
}
