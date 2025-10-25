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
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Foods",
                columns: new[] { "Id", "Carbohydrates", "CreatedBy", "Fats", "Image", "Kilocalories", "MeasurementQuantity", "Name", "Proteins", "Salt", "Sugar", "UnitOfMeasurement", "UserId" },
                values: new object[,]
                {
                    { 1, 0.0, "Admin", 91.0, "https://www.google.com/url?sa=i&url=https%3A%2F%2Ffinditapp.es%2Fproduct%2F21545%2Faceite-de-oliva-virgen-extra-hacendado&psig=AOvVaw3lJleOAYGiYoGiu_nXPnVO&ust=1761502309066000&source=images&cd=vfe&opi=89978449&ved=0CBUQjRxqFwoTCPDws8_5v5ADFQAAAAAdAAAAABAE", 822, 100, "Aceite de oliva virgen extra Hacendado", 0.0, 0.0, 0.0, 1, 1 },
                    { 2, 4.5999999999999996, "Admin", 3.6000000000000001, "https://www.google.com/url?sa=i&url=https%3A%2F%2Fsuper.facua.org%2Fmercadona%2Fleche%2Fleche-entera-hacendado-1-l%2F&psig=AOvVaw3kHUfgmwIYi6NkahqgqyVE&ust=1761502634457000&source=images&cd=vfe&opi=89978449&ved=0CBUQjRxqFwoTCPCF0Or6v5ADFQAAAAAdAAAAABAE", 63, 100, "Leche entera Hacendado", 3.1000000000000001, 0.13, 4.5999999999999996, 1, 1 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Age", "Country", "Email", "Height", "Name", "Password", "Role", "Weight" },
                values: new object[,]
                {
                    { 1, 1, "España", "admin@nutricore.com", 30, "Admin", "YHrp/ExR53lRO6ouA2tT0y9QCb94jfjNBsxcGq5x798=", "admin", 1 },
                    { 2, 24, "España", "mario@gmail.com", 170, "Mario", "JApd9lfG2wshq3agTXjgwVT/f4jQecLCYTBnBT30AqE=", "user", 65 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Foods");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
