using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Playing
{
    public class Game
    {
        public string Id { get; internal set; }

        private List<IEvent> uncommitedEvents = new List<IEvent>();
        public bool IsOpened { get; internal set; } = false;

        public int Level { get; internal set; }

        public void Open(string id)
        {
            IEvent e = new Opened(1, id);
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
        
        public string Id { get; set; }

        public void Handle(Game game)
        {
            if (Id == null)
            {
                throw new Exception("Missing command parameter 'id'");
            }
            if (game != null && game.IsOpened)
            {
                throw new Exception("Game already open");
            }

            game.Open(Id);
        }
    }

    [Aggregate(typeof(Game))]
    public class Close : ICommand
    {

        public string Id { get; set; }

        public void Handle(Game game)
        {
            
        }
    }

    public class Opened : IEvent
    {
        private readonly int _level;

        private readonly string _id;

        public string Id => _id;

        public int Level => _level;

        public Opened(int level, string id)
        {
            _level = level;
            _id = id;
        }

        public void Handle(Game game)
        {
            game.IsOpened = true;
            game.Level = _level;
            game.Id = _id;
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
