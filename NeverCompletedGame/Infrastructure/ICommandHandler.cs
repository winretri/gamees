using System;
using System.Collections.Generic;
using System.Text;
using Playing;
using Playing.Commands;

namespace Infrastructure
{
    public interface ICommandHandler<in T> where T: ICommand
    {
        void Handle(string aggregateId, ICommand command);
    }
}
