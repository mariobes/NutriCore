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
    public DbSet<Intake> Intakes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasData(
            new User { Id = 1, Name = "Admin", Email = "admin@nutricore.com", Password = "dSvr3S6iXPjFcyMth0rbwQ==.u/1Q7+g3u40Ri7bJuIF22ABJzTPhcFsO2X5kTKCnObw=", Age = 1, Height = 30, Weight = 1, Country = "España", DailyWater = 0, DateDailyWater = new DateTime(0001, 1, 1), DailyKilocalorieTarget = 0, DailyFatTarget = 0, DailyCarbohydrateTarget = 0, DailyProteinTarget = 0, DailyWaterTarget = 0, Role = Roles.Admin },
            new User { Id = 2, Name = "Mario", Email = "mario@gmail.com", Password = "4HsR7ujr1mOgg8fgDi0T/A==.E4PTkbfOEjDLf2f6FsoelJuUUFeqq1/H2sdeitKpb7E=", Age = 24, Height = 170, Weight = 65, Country = "España", DailyWater = 1.5, DateDailyWater = new DateTime(2026, 2, 11), DailyKilocalorieTarget = 2300, DailyFatTarget = 60, DailyCarbohydrateTarget = 160, DailyProteinTarget = 130, DailyWaterTarget = 3 }
        );

        modelBuilder.Entity<Food>().HasData(
            new Food { Id = 1, UserId = 1, Name = "Aceite de oliva virgen extra Hacendado", Image = "https://dx7csy7aghu7b.cloudfront.net/prods/7567722.webp", UnitOfMeasurement = EnumUnitOfMeasurementOptions.Milliliters, MeasurementQuantity = 100, Kilocalories = 822, Fats = 91, Carbohydrates = 0, Proteins = 0, Fiber = 0, Sugar = 0, Salt = 0, CreatedBy = "admin" },
            new Food { Id = 2, UserId = 1, Name = "Leche entera Hacendado", Image = "https://prod-mercadona.imgix.net/images/40d2c64941b80f76dce672a3eab794a2.jpg?fit=crop&h=600&w=600", UnitOfMeasurement = EnumUnitOfMeasurementOptions.Milliliters, MeasurementQuantity = 100, Kilocalories = 63, Fats = 3.6, Carbohydrates = 4.6, Proteins = 3.1, Fiber = 0, Sugar = 4.6, Salt = 0.13, CreatedBy = "admin" },
            new Food { Id = 3, UserId = 1, Name = "Huevos", Image = "https://prod-mercadona.imgix.net/images/bdad77c847511bc5d6fa8e5fcc533823.jpg?fit=crop&h=600&w=600", UnitOfMeasurement = EnumUnitOfMeasurementOptions.Grams, MeasurementQuantity = 100, Kilocalories = 150, Fats = 11.1, Carbohydrates = 0, Proteins = 12.5, Fiber = 0, Sugar = 0, Salt = 0.36, CreatedBy = "admin" },
            new Food { Id = 4, UserId = 1, Name = "Jamón de Trevélez", Image = "https://prod-mercadona.imgix.net/images/5513997f44d87852326a373071baec5b.jpg?fit=crop&h=300&w=300", UnitOfMeasurement = EnumUnitOfMeasurementOptions.Grams, MeasurementQuantity = 100, Kilocalories = 268, Fats = 15, Carbohydrates = 0, Proteins = 33, Fiber = 0, Sugar = 0, Salt = 3.9, CreatedBy = "admin" },
            new Food { Id = 5, UserId = 1, Name = "Patatas", Image = "https://images.openfoodfacts.org/images/products/084/000/069/4713/front_es.9.400.jpg", UnitOfMeasurement = EnumUnitOfMeasurementOptions.Grams, MeasurementQuantity = 100, Kilocalories = 62, Fats = 0, Carbohydrates = 12.4, Proteins = 1.8, Fiber = 2.3, Sugar = 2, Salt = 0, CreatedBy = "admin" }
        );
        
        modelBuilder.Entity<Meal>().HasData(
            new Meal { Id = 1, UserId = 1, Name = "Huevos rotos con jamón", Image = "https://newluxbrand.com/recetas/wp-content/uploads/2023/04/Abril23_V55_huevosrotosconjamon_01.jpg", TotalKilocalories = 323, TotalFats = 15.6, TotalCarbohydrates = 18.6, TotalProteins = 25.1, TotalFiber = 3.45, TotalSugar = 3, TotalSalt = 1.53, CreatedBy = "admin" }
        );

        modelBuilder.Entity<MealIngredient>().HasData(
            new MealIngredient { Id = 1, MealId = 1, FoodId = 3, Quantity = 100 },
            new MealIngredient { Id = 2, MealId = 1, FoodId = 4, Quantity = 30 },
            new MealIngredient { Id = 3, MealId = 1, FoodId = 5, Quantity = 150 }
        );

        modelBuilder.Entity<Intake>().HasData(
            new Intake { Id = 1, UserId = 2, ConsumableId = 4, ConsumableType = "food", Date = new DateTime(2026, 2, 11), FoodQuantity = 100, TotalKilocalories = 268, TotalFats = 15, TotalCarbohydrates = 0, TotalProteins = 33, TotalFiber = 0, TotalSugar = 0, TotalSalt = 3.9 }
        );
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .LogTo(Console.WriteLine, LogLevel.Information)
            .EnableSensitiveDataLogging();
    }
}