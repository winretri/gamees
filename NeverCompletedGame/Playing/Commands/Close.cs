namespace Playing.Commands
{
    [Aggregate(typeof(Game))]
    public class Close : ICommand
    {
        #region Public Properties

        public string Id { get; set; }

        #endregion

        #region Public Methods and Operators

        public void Handle(Game game)
        {
        }

        #endregion
    }
}