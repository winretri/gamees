using System;
using System.Collections.Generic;
using System.Text;
using Playing;

namespace Infrastructure
{
    public interface IRepository
    {
        void Save(Aggregate game);

        Aggregate Restore(string id);
    }
}
