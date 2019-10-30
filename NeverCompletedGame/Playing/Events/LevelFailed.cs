using System.Dynamic;

namespace Playing.Events
{
    public class LevelFailed : IEvent
    {
        #region Constructors and Destructors

        public LevelFailed(string id, int newScore, string guessId)
        {
            this.Id = id;
            this.NewScore = newScore;
            this.GuessId = guessId;
        }

        #endregion

        #region Public Properties

        public string Id { get; }

        public string GuessId { get; }

        public int NewScore { get; }

        #endregion

        #region Public Methods and Operators

        public void Handle(Game game)
        {
            game.Score = NewScore;
        }

        #endregion
    }
}