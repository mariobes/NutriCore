using Microsoft.EntityFrameworkCore;
using NutriCore.Models;
using Microsoft.Extensions.Logging;

namespace NutriCore.Data;

public class NutriCoreContext : DbContext
{
    public NutriCoreContext(DbContextOptions<NutriCoreContext> options) : base(options) {}

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasData(
            new User { Id = 1, Name = "Admin", Email = "admin@nutricore.com", Password = "YHrp/ExR53lRO6ouA2tT0y9QCb94jfjNBsxcGq5x798=", Age = 1, Height = 30, Weight = 1, Country = "España", Role = Roles.Admin },
            new User { Id = 2, Name = "Mario", Email = "mario@gmail.com", Password = "JApd9lfG2wshq3agTXjgwVT/f4jQecLCYTBnBT30AqE=", Age = 24, Height = 170, Weight = 65, Country = "España" }
        );
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .LogTo(Console.WriteLine, LogLevel.Information)
            .EnableSensitiveDataLogging();
    }
}