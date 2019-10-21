using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Views
{
    public class GameViewDbContext : DbContext
    {
        public DbSet<GameReadModel> Games;
    }
}
