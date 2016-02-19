using System;
using Microsoft.Data.Entity;

namespace theworld.Models
{
    public class WorldContext : DbContext
    {
        protected WorldContext()
        {
            Database.EnsureCreated();
        }

        public DbSet<Stop> Stops { get; set; }

        public DbSet<Trip> Trips { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = Startup.Configuration["Data:WorldContextConnection"];
            optionsBuilder.UseSqlServer(connectionString);
            base.OnConfiguring(optionsBuilder);
        }
    }
}
