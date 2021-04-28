using System;
using Microsoft.EntityFrameworkCore;
using Rocket.Models;

namespace Rocket.Repositories
{
    public class RocketDbContext : DbContext
    {
        public RocketDbContext(DbContextOptions<RocketDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Contest> Contests { get; set; }
        public DbSet<Bet> Bets { get; set; }

        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>()
                .HasMany(x => x.Bets)
                .WithOne();
            builder.Entity<User>()
                .HasMany(x => x.Transactions)
                .WithOne();

            builder.Entity<Contest>()
                .HasMany(x => x.Bets)
                .WithOne();

        }
    }
}
