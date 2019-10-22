using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Views
{
    public class GameViewDbContext : DbContext
    {
        public GameViewDbContext(DbContextOptions<GameViewDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<GameReadModel>()
                .HasKey(grm => grm.Id);
        }

        public DbSet<GameReadModel> Games { get; set; }
    }
}
