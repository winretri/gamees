using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using Playing;

namespace Infrastructure.EventEmitter
{
    public class EventEmitter : IEventBus
    {
        public event EventHandler<IEvent> On;

        public EventEmitter()
        {
            Events = Observable.FromEventPattern<IEvent>(ev => On += ev,
                ev => On -= ev);
        }

        public IObservable<EventPattern<IEvent>> Events { get; }

        public void Emit(IEnumerable<IEvent> @events)
        {
            foreach (var @event in @events)
            {
                Emit(@event);
            }
        }

        public void Emit(IEvent e)
        {
            On?.Invoke( this, e);
        }
    }
}
