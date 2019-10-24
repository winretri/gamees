using System;
using System.Collections.Generic;
using System.Text;

namespace Playing.Events
{
    public class GameCompleted : IEvent
    {
        public GameCompleted(string id)
        {
            Id = id;
        }

        public string Id { get; internal set; }

        public void Handle(Game game)
        {
            game.IsCompleted = true;
        }
    }
}
