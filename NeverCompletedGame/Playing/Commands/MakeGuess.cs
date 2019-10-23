using System;

namespace Playing.Commands
{
    [Aggregate(typeof(Game))]
    public class MakeGuess : ICommand
    {
        #region Public Properties

        public string Guess { get; set; }

        #endregion

        #region Public Methods and Operators

        public void Handle(Game game)
        {
            if (Guess == null)
            {
                throw new Exception("Missing command parameter 'guess'");
            }

            if (game != null && !game.IsOpened)
            {
                throw new Exception("Game is not open");
            }

            game.MakeGuess(Guess);
        }

        #endregion
    }
}