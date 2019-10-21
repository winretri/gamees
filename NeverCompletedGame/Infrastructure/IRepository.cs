using System;
using System.Collections.Generic;
using System.Text;
using Playing;

namespace Infrastructure
{
    public interface IRepository
    {
        void Save(Game game);

        Game Restore(string id);
    }
}
