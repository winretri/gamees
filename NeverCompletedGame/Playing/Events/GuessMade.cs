namespace Playing.Events
{
    public class GuessMade : IEvent
    {
        #region Constructors and Destructors

        public GuessMade(string id,
            string guess,
            int level)
        {
            Guess = guess;
            Id = id;
            Level = level;
        }

        #endregion

        #region Public Properties

        public string Guess { get; }

        public string Id { get; }

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