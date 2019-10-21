using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeverCompletedGame.Infrastructure
{
    public interface ICommandFactory
    {
        T GetCommand<T>(string domain,
            string aggregate,
            string command,
            string payload);
    }
}
