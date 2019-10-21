using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Playing;

namespace Infrastructure.Riddles
{
    public interface IRiddleRepository
    {
        Riddle GetRiddle(int level);
    }
}
