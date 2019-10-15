using System;
using System.Collections.Generic;
using System.Text;
using Domain;

namespace Infrastructure
{
    public interface IRepository
    {
        void Save(Game game);
    }
}
