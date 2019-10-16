﻿using System;
using System.Collections.Generic;
using System.Text;
using Playing;

namespace Infrastructure
{
    interface ICommandHandler<T> where T: ICommand
    {
        void Handle(T command);
    }
}
