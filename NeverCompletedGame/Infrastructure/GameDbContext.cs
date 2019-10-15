using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class GameDbContext : DbContext
    {
        public DbSet<EventStoreEvent> Events;
    }
}
