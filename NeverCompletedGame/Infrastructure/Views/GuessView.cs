using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Views
{
    public class GuessView
    {
        public string Id { get; internal set; }

        public string GameId { get; internal set; }

        public string Guess { get; internal set; }

        public string GuessStatus { get; internal set; }
    }
}
