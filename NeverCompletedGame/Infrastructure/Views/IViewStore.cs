using System;
using System.Collections.Generic;
using System.Text;
using Playing;
using Playing.Events;

namespace Infrastructure.Views
{
    public interface IViewStore
    {
        void Handle(IEvent e);

        GameView Get(string id);
    }
}
