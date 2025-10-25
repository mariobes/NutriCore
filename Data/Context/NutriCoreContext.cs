using Microsoft.EntityFrameworkCore;
using NutriCore.Models;
using Microsoft.Extensions.Logging;

namespace NutriCore.Data;

public class NutriCoreContext : DbContext
{
    public NutriCoreContext(DbContextOptions<NutriCoreContext> options) : base(options) {}

    public DbSet<User> Users { get; set; }
    public DbSet<Food> Foods { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasData(
            new User { Id = 1, Name = "Admin", Email = "admin@nutricore.com", Password = "YHrp/ExR53lRO6ouA2tT0y9QCb94jfjNBsxcGq5x798=", Age = 1, Height = 30, Weight = 1, Country = "España", Role = Roles.Admin },
            new User { Id = 2, Name = "Mario", Email = "mario@gmail.com", Password = "JApd9lfG2wshq3agTXjgwVT/f4jQecLCYTBnBT30AqE=", Age = 24, Height = 170, Weight = 65, Country = "España" }
        );

        modelBuilder.Entity<Food>().HasData(
            new Food { Id = 1, UserId = 1, Name = "Aceite de oliva virgen extra Hacendado", Image = "https://dx7csy7aghu7b.cloudfront.net/prods/7567722.webp", UnitOfMeasurement = EnumUnitOfMeasurementOptions.Milliliters, MeasurementQuantity = 100, Kilocalories = 822, Fats = 91, Carbohydrates = 0, Proteins = 0, Sugar = 0, Salt = 0, CreatedBy = "Admin" },
            new Food { Id = 2, UserId = 1, Name = "Leche entera Hacendado", Image = "https://prod-mercadona.imgix.net/images/40d2c64941b80f76dce672a3eab794a2.jpg?fit=crop&h=600&w=600", UnitOfMeasurement = EnumUnitOfMeasurementOptions.Milliliters, MeasurementQuantity = 100, Kilocalories = 63, Fats = 3.6, Carbohydrates = 4.6, Proteins = 3.1, Sugar = 4.6, Salt = 0.13, CreatedBy = "Admin" }
        );
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .LogTo(Console.WriteLine, LogLevel.Information)
            .EnableSensitiveDataLogging();
    }
}