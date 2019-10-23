namespace Playing.Commands
{
    public interface ICommand
    {
        #region Public Methods and Operators

        void Handle(Game game);

        #endregion
    }
}