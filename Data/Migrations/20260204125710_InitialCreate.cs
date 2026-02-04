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

            migrationBuilder.InsertData(
                table: "Foods",
                columns: new[] { "Id", "Carbohydrates", "CreatedBy", "Fats", "Image", "Kilocalories", "MeasurementQuantity", "Name", "Proteins", "Salt", "Sugar", "UnitOfMeasurement", "UserId" },
                values: new object[,]
                {
                    { 1, 0.0, "Admin", 91.0, "https://dx7csy7aghu7b.cloudfront.net/prods/7567722.webp", 822, 100, "Aceite de oliva virgen extra Hacendado", 0.0, 0.0, 0.0, 1, 1 },
                    { 2, 4.5999999999999996, "Admin", 3.6000000000000001, "https://prod-mercadona.imgix.net/images/40d2c64941b80f76dce672a3eab794a2.jpg?fit=crop&h=600&w=600", 63, 100, "Leche entera Hacendado", 3.1000000000000001, 0.13, 4.5999999999999996, 1, 1 }
                });

            migrationBuilder.InsertData(
                table: "Meals",
                columns: new[] { "Id", "CreatedBy", "Image", "Name", "UserId" },
                values: new object[] { 1, "Admin", "https://newluxbrand.com/recetas/wp-content/uploads/2023/04/Abril23_V55_huevosrotosconjamon_01.jpg", "Huevos rotos con jamón", 1 });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Age", "Country", "DailyCarbohydrateTarget", "DailyFatTarget", "DailyKilocalorieTarget", "DailyProteinTarget", "DailyWater", "DailyWaterTarget", "Email", "Height", "Name", "Password", "Role", "Weight" },
                values: new object[,]
                {
                    { 1, 1, "España", 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, "admin@nutricore.com", 30, "Admin", "dSvr3S6iXPjFcyMth0rbwQ==.u/1Q7+g3u40Ri7bJuIF22ABJzTPhcFsO2X5kTKCnObw=", "admin", 1 },
                    { 2, 24, "España", 160.0, 60.0, 2300.0, 130.0, 1.5, 3.0, "mario@gmail.com", 170, "Mario", "4HsR7ujr1mOgg8fgDi0T/A==.E4PTkbfOEjDLf2f6FsoelJuUUFeqq1/H2sdeitKpb7E=", "user", 65 }
                });

            migrationBuilder.InsertData(
                table: "MealIngredients",
                columns: new[] { "Id", "FoodId", "MealId", "Quantity" },
                values: new object[,]
                {
                    { 1, 1, 1, 10.0 },
                    { 2, 2, 1, 50.0 }
                });

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
