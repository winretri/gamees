﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Playing.Commands;
using Playing.Events;

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
                IEvent levelSucceeded = new LevelSucceeded(this.Id, this.Level + 1, this.Score + 1 * Level);
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
}