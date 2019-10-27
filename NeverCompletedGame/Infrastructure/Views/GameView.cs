using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Views
{
    public class GameView
    {
        public string Id { get; internal set; }

        public int Level { get; internal set; }

        public string Question { get; internal set; }

        public int Score { get; internal set; }

        public bool Completed { get; internal set; }
    }
}
