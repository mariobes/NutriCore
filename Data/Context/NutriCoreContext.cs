using Microsoft.EntityFrameworkCore;
using NutriCore.Models;
using Microsoft.Extensions.Logging;

namespace NutriCore.Data;

public class NutriCoreContext : DbContext
{
    public NutriCoreContext(DbContextOptions<NutriCoreContext> options) : base(options) {}

    public DbSet<User> Users { get; set; }
    public DbSet<Food> Foods { get; set; }
    public DbSet<Meal> Meals { get; set; }
    public DbSet<MealIngredient> MealIngredients { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasData(
            new User { Id = 1, Name = "Admin", Email = "admin@nutricore.com", Password = "dSvr3S6iXPjFcyMth0rbwQ==.u/1Q7+g3u40Ri7bJuIF22ABJzTPhcFsO2X5kTKCnObw=", Age = 1, Height = 30, Weight = 1, Country = "España", DailyWater = 0, DailyKilocalorieTarget = 0, DailyFatTarget = 0, DailyCarbohydrateTarget = 0, DailyProteinTarget = 0, DailyWaterTarget = 0, Role = Roles.Admin },
            new User { Id = 2, Name = "Mario", Email = "mario@gmail.com", Password = "4HsR7ujr1mOgg8fgDi0T/A==.E4PTkbfOEjDLf2f6FsoelJuUUFeqq1/H2sdeitKpb7E=", Age = 24, Height = 170, Weight = 65, Country = "España", DailyWater = 1.5, DailyKilocalorieTarget = 2300, DailyFatTarget = 60, DailyCarbohydrateTarget = 160, DailyProteinTarget = 130, DailyWaterTarget = 3 }
        );

        modelBuilder.Entity<Food>().HasData(
            new Food { Id = 1, UserId = 1, Name = "Aceite de oliva virgen extra Hacendado", Image = "https://dx7csy7aghu7b.cloudfront.net/prods/7567722.webp", UnitOfMeasurement = EnumUnitOfMeasurementOptions.Milliliters, MeasurementQuantity = 100, Kilocalories = 822, Fats = 91, Carbohydrates = 0, Proteins = 0, Sugar = 0, Salt = 0, CreatedBy = "Admin" },
            new Food { Id = 2, UserId = 1, Name = "Leche entera Hacendado", Image = "https://prod-mercadona.imgix.net/images/40d2c64941b80f76dce672a3eab794a2.jpg?fit=crop&h=600&w=600", UnitOfMeasurement = EnumUnitOfMeasurementOptions.Milliliters, MeasurementQuantity = 100, Kilocalories = 63, Fats = 3.6, Carbohydrates = 4.6, Proteins = 3.1, Sugar = 4.6, Salt = 0.13, CreatedBy = "Admin" }
        );
        
        modelBuilder.Entity<Meal>().HasData(
            new Meal { Id = 1, UserId = 1, Name = "Huevos rotos con jamón", Image = "https://newluxbrand.com/recetas/wp-content/uploads/2023/04/Abril23_V55_huevosrotosconjamon_01.jpg", CreatedBy = "Admin" }
        );

        modelBuilder.Entity<MealIngredient>().HasData(
            new MealIngredient { Id = 1, MealId = 1, FoodId = 1, Quantity = 10 },
            new MealIngredient { Id = 2, MealId = 1, FoodId = 2, Quantity = 50 }
        );
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .LogTo(Console.WriteLine, LogLevel.Information)
            .EnableSensitiveDataLogging();
    }
}