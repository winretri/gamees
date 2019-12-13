using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Playing.Commands;
using Playing.Events;

namespace Playing
{
    public class Game
    {
        #region Fields
        
        public event EventHandler<IEvent> On;

        public Game() {
            Events = Observable.FromEventPattern<IEvent>(ev => On += ev,
                ev => On -= ev);
        }

        public IObservable<EventPattern<IEvent>> Events { get; }

        #endregion

        #region Public Properties

        public static IEnumerable<Type> DomainCommandTypes
        {
            get { return Assembly.GetExecutingAssembly().GetTypes().Where(t => typeof(ICommand).IsAssignableFrom(t)); }
        }

        public static IEnumerable<Type> DomainEventTypes
        {
            get { return Assembly.GetExecutingAssembly().GetTypes().Where(t => typeof(IEvent).IsAssignableFrom(t)); }
        }

        public int Attempts { get; internal set; }

        public Riddle CurrentRiddle => GetRiddle(Level);

        public string Id { get; internal set; }

        public bool IsOpened { get; internal set; } = false;

        public bool IsCompleted { get; internal set; } = false;

        public int Level { get; internal set; }

        public int Score { get; internal set; }


        #endregion

        #region Public Methods and Operators
        #region helpers
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

        public static int MaxLevel
        {
            get
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
                            return riddles.Max(riddle => riddle.Level);
                        }
                    }
                }
            }
        }
        #endregion
        public void MakeGuess(string guess)
        {
            GuessMade e = new GuessMade(Guid.NewGuid().ToString(), this.Id, guess, this.Level);
            e.Handle(this);
            PublishEvent(e);
            if (CurrentRiddle.Solution.Trim().Equals(guess))
            {
                bool completed = this.Level == MaxLevel;
                int nextLevel = completed ? this.Level : this.Level + 1;
                IEvent levelSucceeded = new LevelSucceeded(this.Id, this.Level, nextLevel, this.Score + 1 * Level, e.GuessId);
                levelSucceeded.Handle(this);
                PublishEvent(levelSucceeded);
                if (completed)
                {
                    IEvent gameCompleted = new GameCompleted(this.Id);
                    gameCompleted.Handle(this);
                    PublishEvent(gameCompleted);
                }
            }
            else
            {
                int newScore = (this.Score - 1) < 0 ? 0 : this.Score - 1;
                IEvent levelFailed = new LevelFailed(this.Id, newScore, e.GuessId);
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
            On?.Invoke(this,e);
        }

        #endregion
    }
}