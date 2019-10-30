using System.Collections.Generic;
using Infrastructure.Events;

namespace Infrastructure.Views
{
    public interface IViewStore
    {
        void Handle(IEventSourcingEvent e);

        GameView Get(string id);

        IList<GuessView> GetGuesses(string id, int level);
    }
}
