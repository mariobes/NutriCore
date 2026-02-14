using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NutriCore.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Foods",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnitOfMeasurement = table.Column<int>(type: "int", nullable: false),
                    MeasurementQuantity = table.Column<int>(type: "int", nullable: false),
                    Kilocalories = table.Column<int>(type: "int", nullable: false),
                    Fats = table.Column<double>(type: "float", nullable: false),
                    Carbohydrates = table.Column<double>(type: "float", nullable: false),
                    Proteins = table.Column<double>(type: "float", nullable: false),
                    Fiber = table.Column<double>(type: "float", nullable: false),
                    Sugar = table.Column<double>(type: "float", nullable: false),
                    Salt = table.Column<double>(type: "float", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Foods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Meals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalKilocalories = table.Column<int>(type: "int", nullable: false),
                    TotalFats = table.Column<double>(type: "float", nullable: false),
                    TotalCarbohydrates = table.Column<double>(type: "float", nullable: false),
                    TotalProteins = table.Column<double>(type: "float", nullable: false),
                    TotalFiber = table.Column<double>(type: "float", nullable: false),
                    TotalSugar = table.Column<double>(type: "float", nullable: false),
                    TotalSalt = table.Column<double>(type: "float", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Height = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<int>(type: "int", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DailyWater = table.Column<double>(type: "float", nullable: false),
                    DateDailyWater = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DailyKilocalorieTarget = table.Column<double>(type: "float", nullable: false),
                    DailyFatTarget = table.Column<double>(type: "float", nullable: false),
                    DailyCarbohydrateTarget = table.Column<double>(type: "float", nullable: false),
                    DailyProteinTarget = table.Column<double>(type: "float", nullable: false),
                    DailyWaterTarget = table.Column<double>(type: "float", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MealIngredients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MealId = table.Column<int>(type: "int", nullable: false),
                    FoodId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealIngredients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MealIngredients_Foods_FoodId",
                        column: x => x.FoodId,
                        principalTable: "Foods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MealIngredients_Meals_MealId",
                        column: x => x.MealId,
                        principalTable: "Meals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Intakes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ConsumableId = table.Column<int>(type: "int", nullable: false),
                    ConsumableType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FoodQuantity = table.Column<int>(type: "int", nullable: true),
                    TotalKilocalories = table.Column<int>(type: "int", nullable: false),
                    TotalFats = table.Column<double>(type: "float", nullable: false),
                    TotalCarbohydrates = table.Column<double>(type: "float", nullable: false),
                    TotalProteins = table.Column<double>(type: "float", nullable: false),
                    TotalFiber = table.Column<double>(type: "float", nullable: false),
                    TotalSugar = table.Column<double>(type: "float", nullable: false),
                    TotalSalt = table.Column<double>(type: "float", nullable: false),
                    FoodId = table.Column<int>(type: "int", nullable: true),
                    MealId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Intakes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Intakes_Foods_FoodId",
                        column: x => x.FoodId,
                        principalTable: "Foods",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Intakes_Meals_MealId",
                        column: x => x.MealId,
                        principalTable: "Meals",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Intakes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Foods",
                columns: new[] { "Id", "Carbohydrates", "CreatedBy", "Fats", "Fiber", "Image", "Kilocalories", "MeasurementQuantity", "Name", "Proteins", "Salt", "Sugar", "UnitOfMeasurement", "UserId" },
                values: new object[,]
                {
                    { 1, 0.0, "admin", 91.0, 0.0, "https://dx7csy7aghu7b.cloudfront.net/prods/7567722.webp", 822, 100, "Aceite de oliva virgen extra Hacendado", 0.0, 0.0, 0.0, 1, 1 },
                    { 2, 4.5999999999999996, "admin", 3.6000000000000001, 0.0, "https://prod-mercadona.imgix.net/images/40d2c64941b80f76dce672a3eab794a2.jpg?fit=crop&h=600&w=600", 63, 100, "Leche entera Hacendado", 3.1000000000000001, 0.13, 4.5999999999999996, 1, 1 },
                    { 3, 0.0, "admin", 11.1, 0.0, "https://prod-mercadona.imgix.net/images/bdad77c847511bc5d6fa8e5fcc533823.jpg?fit=crop&h=600&w=600", 150, 100, "Huevos", 12.5, 0.35999999999999999, 0.0, 0, 1 },
                    { 4, 0.0, "admin", 15.0, 0.0, "https://prod-mercadona.imgix.net/images/5513997f44d87852326a373071baec5b.jpg?fit=crop&h=300&w=300", 268, 100, "Jamón de Trevélez", 33.0, 3.8999999999999999, 0.0, 0, 1 },
                    { 5, 12.4, "admin", 0.0, 2.2999999999999998, "https://images.openfoodfacts.org/images/products/084/000/069/4713/front_es.9.400.jpg", 62, 100, "Patatas", 1.8, 0.0, 2.0, 0, 1 }
                });

            migrationBuilder.InsertData(
                table: "Meals",
                columns: new[] { "Id", "CreatedBy", "Image", "Name", "TotalCarbohydrates", "TotalFats", "TotalFiber", "TotalKilocalories", "TotalProteins", "TotalSalt", "TotalSugar", "UserId" },
                values: new object[] { 1, "admin", "https://newluxbrand.com/recetas/wp-content/uploads/2023/04/Abril23_V55_huevosrotosconjamon_01.jpg", "Huevos rotos con jamón", 18.600000000000001, 15.6, 3.4500000000000002, 323, 25.100000000000001, 1.53, 3.0, 1 });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Age", "Country", "DailyCarbohydrateTarget", "DailyFatTarget", "DailyKilocalorieTarget", "DailyProteinTarget", "DailyWater", "DailyWaterTarget", "DateDailyWater", "Email", "Height", "Name", "Password", "Role", "Weight" },
                values: new object[,]
                {
                    { 1, 1, "España", 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@nutricore.com", 30, "Admin", "dSvr3S6iXPjFcyMth0rbwQ==.u/1Q7+g3u40Ri7bJuIF22ABJzTPhcFsO2X5kTKCnObw=", "admin", 1 },
                    { 2, 24, "España", 160.0, 60.0, 2300.0, 130.0, 1.5, 3.0, new DateTime(2026, 2, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "mario@gmail.com", 170, "Mario", "4HsR7ujr1mOgg8fgDi0T/A==.E4PTkbfOEjDLf2f6FsoelJuUUFeqq1/H2sdeitKpb7E=", "user", 65 }
                });

            migrationBuilder.InsertData(
                table: "Intakes",
                columns: new[] { "Id", "ConsumableId", "ConsumableType", "Date", "FoodId", "FoodQuantity", "MealId", "TotalCarbohydrates", "TotalFats", "TotalFiber", "TotalKilocalories", "TotalProteins", "TotalSalt", "TotalSugar", "UserId" },
                values: new object[] { 1, 4, "food", new DateTime(2026, 2, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 100, null, 0.0, 15.0, 0.0, 268, 33.0, 3.8999999999999999, 0.0, 2 });

            migrationBuilder.InsertData(
                table: "MealIngredients",
                columns: new[] { "Id", "FoodId", "MealId", "Quantity" },
                values: new object[,]
                {
                    { 1, 3, 1, 100.0 },
                    { 2, 4, 1, 30.0 },
                    { 3, 5, 1, 150.0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Intakes_FoodId",
                table: "Intakes",
                column: "FoodId");

            migrationBuilder.CreateIndex(
                name: "IX_Intakes_MealId",
                table: "Intakes",
                column: "MealId");

            migrationBuilder.CreateIndex(
                name: "IX_Intakes_UserId",
                table: "Intakes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MealIngredients_FoodId",
                table: "MealIngredients",
                column: "FoodId");

            migrationBuilder.CreateIndex(
                name: "IX_MealIngredients_MealId",
                table: "MealIngredients",
                column: "MealId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Intakes");

            migrationBuilder.DropTable(
                name: "MealIngredients");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Foods");

            migrationBuilder.DropTable(
                name: "Meals");
        }
    }
}
