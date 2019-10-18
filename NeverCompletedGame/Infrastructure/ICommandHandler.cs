using System;
using System.Collections.Generic;
using System.Text;
using Playing;

namespace Infrastructure
{
    public interface ICommandHandler<T> where T: ICommand
    {
        void Handle(T command);
    }
}
