namespace Playing.Events
{
    public class Opened : IEvent
    {
        #region Fields

        public int Score = 0;

        private readonly string _id;
        private readonly int _level;

        #endregion

        #region Constructors and Destructors

        public Opened(int level,
            string id)
        {
            _level = level;
            _id = id;
        }

        #endregion

        #region Public Properties

        public string Id => _id;

        public int Level => _level;

        #endregion

        #region Public Methods and Operators

        public void Handle(Game game)
        {
            game.IsOpened = true;
            game.Level = _level;
            game.Id = _id;
            game.Score = 0;
        }

        #endregion
    }
}