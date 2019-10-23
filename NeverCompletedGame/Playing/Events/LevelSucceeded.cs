namespace Playing.Events
{
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
}