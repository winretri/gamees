using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;

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

        public int Attempts { get; internal set; }

        public Riddle CurrentRiddle => GetRiddle(Level);

        public string Id { get; internal set; }
        public bool IsOpened { get; internal set; } = false;

        public int Level { get; internal set; }

        public int Score { get; internal set; }

        public IEnumerable<IEvent> UncomittedEvents => new List<IEvent>(uncommitedEvents);

        #endregion

        #region Public Methods and Operators

        public static Riddle GetRiddle(int level)
        {
            var ass = typeof(Riddle).GetTypeInfo().Assembly;
            using (Stream resource = ass.GetManifestResourceStream("Playing.riddles.json"))
            {
                using (var streamReader = new StreamReader(resource))
                {
                    using (var jsonTextReader = new JsonTextReader(streamReader))
                    {
                        JsonSerializer js = JsonSerializer.Create();
                        List<Riddle> riddles = js.Deserialize<List<Riddle>>(jsonTextReader);
                        Riddle riddle = riddles.FirstOrDefault(r => r.Level == level);
                        return riddle;
                    }
                }
            }
        }

        public void MakeGuess(string guess)
        {
            IEvent e = new GuessMade(this.Id, guess);
            e.Handle(this);
            PublishEvent(e);
            if (CurrentRiddle.Solution.Trim().Equals(guess))
            {
                IEvent levelSucceeded = new LevelSucceeded(this.Id,this.Level + 1, this.Score + 1*Level);
                levelSucceeded.Handle(this);
                PublishEvent(levelSucceeded);
            }
            else
            {
                int newScore = (this.Score - 1) < 0 ? 0 : this.Score - 1;
                IEvent levelFailed = new LevelFailed(this.Id, newScore);
                levelFailed.Handle(this);
                PublishEvent(levelFailed);
            }
        }

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
    public class MakeGuess : ICommand
    {
        #region Public Properties

        public string Guess { get; set; }

        #endregion

        #region Public Methods and Operators

        public void Handle(Game game)
        {
            if (Guess == null)
            {
                throw new Exception("Missing command parameter 'guess'");
            }

            if (game != null && !game.IsOpened)
            {
                throw new Exception("Game is not open");
            }

            game.MakeGuess(Guess);
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

    public class GuessMade : IEvent
    {
        #region Constructors and Destructors

        public GuessMade(string id,
            string guess)
        {
            Guess = guess;
            Id = id;
        }

        #endregion

        #region Public Properties

        public string Guess { get; }

        public string Id { get; }

        #endregion

        #region Public Methods and Operators

        public void Handle(Game game)
        {
            game.Attempts += 1;
        }

        #endregion
    }

    public class LevelSucceeded : IEvent
    {
        #region Constructors and Destructors

        public LevelSucceeded(string id, int newLevel, int newScore)
        {
            this.Id = id;
            this.NewLevel = newLevel;
            this.NewScore = newScore;
        }

        #endregion

        #region Public Properties

        public string Id { get; }

        public int NewLevel { get; }

        public int NewScore { get; }

        #endregion

        #region Public Methods and Operators

        public void Handle(Game game)
        {
            game.Level = NewLevel;
            game.Score = NewScore;
        }

        #endregion
    }

    public class LevelFailed : IEvent
    {
        #region Constructors and Destructors

        public LevelFailed(string id, int newScore)
        {
            this.Id = id;
            this.NewScore = newScore;
        }

        #endregion

        #region Public Properties

        public string Id { get; }

        public int NewScore { get; }

        #endregion

        #region Public Methods and Operators

        public void Handle(Game game)
        {
            game.Score = NewScore;
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