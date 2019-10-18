using System;
using System.Collections.Generic;
using System.Text;
using Playing;

namespace Infrastructure
{
    public interface ICommandHandlerFactory
    {
        ICommandHandler<T> GetCommandHandler<T>(string domain, string aggregate, string command) where T : ICommand;
    }
}
