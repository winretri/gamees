namespace Playing.Events
{
    public class LevelSucceeded : IEvent
    {
        #region Constructors and Destructors

        public LevelSucceeded(string id,
            int completedLevel,
            int newLevel,
            int newScore,
            string guessId)
        {
            this.Id = id;
            this.CompletedLevel = completedLevel;
            this.NewLevel = newLevel;
            this.NewScore = newScore;
            this.GuessId = guessId;
        }

        #endregion

        #region Public Properties

        public int CompletedLevel { get; }

        public string GuessId { get; }

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
}