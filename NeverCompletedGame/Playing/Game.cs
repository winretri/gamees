using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Playing
{
    public class Game
    {
        #region Fields

        private List<IEvent> uncommitedEvents = new List<IEvent>();

        #endregion

        #region Public Properties

        public static IEnumerable<Type> DomainCommandTypes
        {
            get { return Assembly.GetExecutingAssembly().GetTypes().Where(t => t.IsAssignableFrom(typeof(ICommand))); }
        }

        public static IEnumerable<Type> DomainEventTypes
        {
            get { return Assembly.GetExecutingAssembly().GetTypes().Where(t => t.IsAssignableFrom(typeof(IEvent))); }
        }

        public string Id { get; internal set; }
        public bool IsOpened { get; internal set; } = false;

        public int Level { get; internal set; }

        public int Score { get; internal set; }

        public IEnumerable<IEvent> UncomittedEvents => new List<IEvent>(uncommitedEvents);

        #endregion

        #region Public Methods and Operators

        public void Open(string id)
        {
            IEvent e = new Opened(1, id);
            e.Handle(this);
            PublishEvent(e);
        }

        #endregion

        #region Methods

        internal void PublishEvent(IEvent e)
        {
            uncommitedEvents.Add(e);
        }

        #endregion
    }

    [Aggregate(typeof(Game))]
    public class Open : ICommand
    {
        #region Public Properties

        public string Id { get; set; }

        #endregion

        #region Public Methods and Operators

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

        #endregion
    }

    [Aggregate(typeof(Game))]
    public class Close : ICommand
    {
        #region Public Properties

        public string Id { get; set; }

        #endregion

        #region Public Methods and Operators

        public void Handle(Game game)
        {
        }

        #endregion
    }

    public class Opened : IEvent
    {
        #region Fields

        public int Score = 0;

        private readonly string _id;
        private readonly int _level;

        #endregion

        #region Constructors and Destructors

        public Opened(int level,
            string id)
        {
            _level = level;
            _id = id;
        }

        #endregion

        #region Public Properties

        public string Id => _id;

        public int Level => _level;

        #endregion

        #region Public Methods and Operators

        public void Handle(Game game)
        {
            game.IsOpened = true;
            game.Level = _level;
            game.Id = _id;
            game.Score = 0;
        }

        #endregion
    }

    public interface ICommand
    {
        #region Public Methods and Operators

        void Handle(Game game);

        #endregion
    }

    public interface IEvent
    {
        #region Public Methods and Operators

        void Handle(Game game);

        #endregion
    }
}