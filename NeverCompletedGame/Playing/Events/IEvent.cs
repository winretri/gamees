namespace Playing.Events
{
    public interface IEvent
    {
        #region Public Methods and Operators

        void Handle(Game game);

        #endregion
    }
}