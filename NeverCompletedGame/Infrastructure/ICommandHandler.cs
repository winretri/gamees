using System;
using System.Collections.Generic;
using System.Text;
using Playing;

namespace Infrastructure
{
    public interface ICommandHandler<in T> where T: ICommand
    {
        void Handle(string aggregateId, T command);
    }
}
