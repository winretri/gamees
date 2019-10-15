using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Domain
{
    public class Game
    {
        public string Id { get; internal set; }

        private List<IEvent> uncommitedEvents = new List<IEvent>();
        public bool IsOpened { get; internal set; } = false;

        public int Level { get; internal set; }

        public void Open()
        {
            IEvent e = new Opened(1);
            e.Handle(this);
            PublishEvent(e);
        }

        internal void PublishEvent(IEvent e)
        {
            uncommitedEvents.Add(e);
        }

        public IEnumerable<IEvent> UncomittedEvents => new List<IEvent>(uncommitedEvents);

        public static IEnumerable<Type> DomainCommandTypes
        {
            get { return Assembly.GetExecutingAssembly().GetTypes().Where(t => t.IsAssignableFrom(typeof(ICommand))); }
        }

        public static IEnumerable<Type> DomainEventTypes
        {
            get { return Assembly.GetExecutingAssembly().GetTypes().Where(t => t.IsAssignableFrom(typeof(IEvent))); }
        }
    }

    [Aggregate(typeof(Game))]
    public class Open : ICommand {
        
        public void Handle(Game game)
        {
            if (game != null && game.IsOpened)
            {
                throw new Exception("Game already open");
            }

            game.Open();
        }
    }

    public class Opened : IEvent
    {
        private readonly int _level;

        public Opened(int level)
        {
            _level = level;
        }

        public void Handle(Game game)
        {
            game.IsOpened = true;
            game.Level = _level;
        }
    }

    public interface ICommand
    {
        void Handle(Game game);
    }

    public interface IEvent
    {
        void Handle(Game game);
    }
}
