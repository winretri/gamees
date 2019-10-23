using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public void Save(Game game)
        {
            var now = DateTime.UtcNow;
            List<EventStoreEvent> esEvents = new List<EventStoreEvent>();
            foreach (IEvent e in game.UncomittedEvents)
            {
                EventStoreEvent ese = new EventStoreEvent();
                ese.AggregateId = game.Id;
                ese.Id = Guid.NewGuid().ToString();
                ese.DomainEventName = e.GetType().Name;
                ese.Time = now;
                ese.EventContainer = JsonConvert.SerializeObject(e);
                esEvents.Add(ese);
            }
            this.context.Events.AddRange(esEvents);
            this.context.SaveChanges();
        }

        public Game Restore(string gameId)
        {
            List<EventStoreEvent> events = this.context.Events.Where(e => e.AggregateId == gameId).OrderBy(e => e.Time).ToList();
            Game g = new Game();
            List<IEvent> domainEvents = events.Select(GetDomainEventForEventStoreEvent).ToList();
            foreach (IEvent domainEvent in domainEvents)
            {
                domainEvent.Handle(g);
            }

            return g;
        }

        public IEvent GetDomainEventForEventStoreEvent(EventStoreEvent e)
        {
            Type domainEventType = Game.DomainEventTypes.First(t => t.Name == e.DomainEventName);
            return (IEvent)JsonConvert.DeserializeObject(e.EventContainer, domainEventType);
        }
    }
}
