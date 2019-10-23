using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.EventEmitter;
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

        public void Handle(IEvent e)
        {
            switch (e)
            {
                case Opened opened:
                    Handle(opened);
                    break;
                case LevelSucceeded succeeded:
                    Handle(succeeded);
                    break;
                case LevelFailed failed:
                    Handle(failed);
                    break;
            }
        }

        private void Handle(Opened opened)
        {
            string aggregateId = opened.Id;
            Handle(aggregateId, opened);
        }

        private void Handle(LevelSucceeded succeeded)
        {
            string aggregateId = succeeded.Id;
            Handle(aggregateId, succeeded);
        }
        private void Handle(LevelFailed failed)
        {
            string aggregateId = failed.Id;
            Handle(aggregateId, failed);
        }

        private void Handle(string aggregateId, IEvent @event)
        {
            _hub.Clients.Group(aggregateId).SendAsync("ReceiveEvent", @event.GetType().Name);
        }

        public void Dispose()
        {
            _subscription?.Dispose();
        }
    }
}
