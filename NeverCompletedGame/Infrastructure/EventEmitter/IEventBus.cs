using System;
using System.Collections.Generic;
using System.Reactive;
using System.Text;
using Playing;

namespace Infrastructure.EventEmitter
{
    public interface IEventBus
    {
        IObservable<EventPattern<IEvent>> Events { get; }

        void Emit(IEnumerable<IEvent> @events);

        void Emit(IEvent @event);
    }
}
