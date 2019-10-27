using Infrastructure.Events;

namespace Infrastructure.Views
{
    public interface IViewStore
    {
        void Handle(IEventSourcingEvent e);

        GameView Get(string id);
    }
}
