using System;
using System.Collections.Generic;
using Infrastructure.Events;
using Newtonsoft.Json;
using Playing;
using Playing.Events;

namespace Infrastructure
{
    public class Aggregate
    {
        #region Fields

        private readonly Game _game;

        private int _sequence = 0;

        private List<EventStoreEvent> uncommitedEvents = new List<EventStoreEvent>();

        #endregion

        #region Constructors and Destructors

        public Aggregate(Game game)
        {
            _game = game;
            _game.Events.Subscribe(args => HandleEvent(args.EventArgs));
        }

        #endregion

        #region Public Properties

        public Game Game => _game;

        public IEnumerable<EventStoreEvent> UncomittedEvents => new List<EventStoreEvent>(uncommitedEvents);

        #endregion

        #region Methods

        private void HandleEvent(IEvent @event)
        {
            IEvent e = @event;
            EventStoreEvent ese = new EventStoreEvent();
            ese.AggregateId = _game.Id;
            ese.Id = Guid.NewGuid().ToString();
            ese.DomainEventName = e.GetType().Name;
            ese.DomainEvent = e;
            ese.Time = DateTime.UtcNow;
            ese.Sequence = _sequence++;
            ese.EventContainer = JsonConvert.SerializeObject(e);
            uncommitedEvents.Add(ese);
        }

        #endregion
    }
}