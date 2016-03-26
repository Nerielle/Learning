using System;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;
using theworld;
using theworld.Models;

namespace theworld.Models
{
  public class WorldContext : IdentityDbContext<WorldUser>
  {
    public WorldContext()
    {
      Database.EnsureCreated();
    }

    public DbSet<Trip> Trips { get; set; }
    public DbSet<Stop> Stops { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      string connectionString = Startup.Configuration["Data:WorldContextConnection"];

      optionsBuilder.UseSqlServer(connectionString);

      base.OnConfiguring(optionsBuilder);
    }
  }
}