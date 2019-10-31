namespace Playing.Events
{
    public class GuessMade : IEvent
    {
        #region Constructors and Destructors

        public GuessMade(string guessId, string gameId,
            string guess,
            int level)
        {
            Guess = guess;
            GameId = gameId;
            Level = level;
            GuessId = guessId;
        }

        #endregion

        #region Public Properties

        public string Guess { get; set; }

        public string GuessId { get; set; } 

        public string GameId { get; }

        public int Level { get; set; }

        #endregion

        #region Public Methods and Operators

        public void Handle(Game game)
        {
            game.Attempts += 1;
        }

        #endregion
    }
}