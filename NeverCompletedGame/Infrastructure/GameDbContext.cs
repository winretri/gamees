using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Events;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class GameDbContext : DbContext
    {
        public GameDbContext(DbContextOptions<GameDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<EventStoreEvent>()
                .HasKey(ese => ese.Id);
        }

        public DbSet<EventStoreEvent> Events { get; set; }
    }
}
