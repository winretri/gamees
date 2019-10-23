using System;

namespace Playing.Commands
{
    [Aggregate(typeof(Game))]
    public class Open : ICommand
    {
        #region Public Properties

        public string Id { get; set; }

        #endregion

        #region Public Methods and Operators

        public void Handle(Game game)
        {
            if (Id == null)
            {
                throw new Exception("Missing command parameter 'id'");
            }

            if (game != null && game.IsOpened)
            {
                throw new Exception("Game already open");
            }

            game.Open(Id);
        }

        #endregion
    }
}