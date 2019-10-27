using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Infrastructure.EventEmitter;
using Infrastructure.Events;
using Microsoft.AspNetCore.SignalR;
using Playing.Events;

namespace NeverCompletedGame.Events
{
    public sealed class EventBusHubBridge : IEventBusHubBridge, IDisposable
    {
        private readonly IHubContext<EventHub> _hub;
        private readonly IDisposable _subscription;
        public EventBusHubBridge(IEventBus eventBus, IHubContext<EventHub> hub)
        {
            _hub = hub;
            _subscription = eventBus.Events.Subscribe(args => Handle(args.EventArgs));
        }

        public void Handle(IEventSourcingEvent e)
        {
            string aggregateId = e.AggregateId;
            Handle(aggregateId, e);
        }

        private void Handle(string aggregateId, IEventSourcingEvent @event)
        {
            string serializedEvent = JsonSerializer.Serialize(@event, typeof(IEventSourcingEvent));
            _hub.Clients.Group(aggregateId).SendAsync("ReceiveEvent", serializedEvent);
        }

        public void Dispose()
        {
            _subscription?.Dispose();
        }

    }
}
