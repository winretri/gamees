using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using Infrastructure.Events;

namespace Infrastructure.EventEmitter
{
    public class EventEmitter : IEventBus
    {
        public event EventHandler<IEventSourcingEvent> On;

        public EventEmitter()
        {
            Events = Observable.FromEventPattern<IEventSourcingEvent>(ev => On += ev,
                ev => On -= ev);
        }

        public IObservable<EventPattern<IEventSourcingEvent>> Events { get; }

        public void Emit(IEnumerable<IEventSourcingEvent> @events)
        {
            foreach (var @event in @events)
            {
                Emit(@event);
            }
        }

        public void Emit(IEventSourcingEvent e)
        {
            On?.Invoke( this, e);
        }
    }
}
