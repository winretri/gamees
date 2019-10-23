namespace Playing.Events
{
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
}