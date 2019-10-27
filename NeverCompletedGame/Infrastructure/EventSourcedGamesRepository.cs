using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure.Events;
using Playing;
using Newtonsoft.Json;
using Playing.Events;

namespace Infrastructure
{
    public class EventSourcedGamesRepository : IRepository
    {
        private GameDbContext context;

        public EventSourcedGamesRepository (GameDbContext context)
        {
            this.context = context;
        }

        public void Save(Aggregate game)
        {
            this.context.Events.AddRange(game.UncomittedEvents);
            this.context.SaveChanges();
        }

        public Aggregate Restore(string gameId)
        {
            List<EventStoreEvent> events = this.context.Events.Where(e => e.AggregateId == gameId).OrderBy(e => e.Time).ThenBy(e => e.Sequence).ToList();
            Game g = new Game();
            List<IEvent> domainEvents = events.Select(GetDomainEventForEventStoreEvent).ToList();
            foreach (IEvent domainEvent in domainEvents)
            {
                domainEvent.Handle(g);
            }

            return new Aggregate(g);;
        }

        public IEvent GetDomainEventForEventStoreEvent(EventStoreEvent e)
        {
            Type domainEventType = Game.DomainEventTypes.First(t => t.Name == e.DomainEventName);
            return (IEvent)JsonConvert.DeserializeObject(e.EventContainer, domainEventType);
        }
    }
}
