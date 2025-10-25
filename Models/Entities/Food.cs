using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NutriCore.Models;

public class Food
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int UserId { get; set; }

    [Required]
    public string? Name { get; set; }

    [Required]
    public string? Image { get; set; }

    [Required]
    [JsonConverter(typeof(EnumDisplayConverter<EnumUnitOfMeasurementOptions>))]
    public EnumUnitOfMeasurementOptions? UnitOfMeasurement { get; set; }

    [Required]
    public int MeasurementQuantity { get; set; }

    [Required]
    public int Kilocalories { get; set; }

    [Required]
    public double Fats { get; set; }

    [Required]
    public double Carbohydrates { get; set; }

    [Required]
    public double Proteins { get; set; }

    [Required]
    public double Sugar { get; set; }

    [Required]
    public double Salt { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? CreatedBy { get; set; } = "User";

    public Food() {}

    public Food(int userId, string name, string image, EnumUnitOfMeasurementOptions unitOfMeasurement, int measurementQuantity,
                int kilocalories, double fats, double carbohydrates, double proteins, double sugar, double salt, string createdBy)
    {
        UserId = userId;
        Name = name;
        Image = image;
        UnitOfMeasurement = unitOfMeasurement;
        MeasurementQuantity = measurementQuantity;
        Kilocalories = kilocalories;
        Fats = fats;
        Carbohydrates = carbohydrates;
        Proteins = proteins;
        Sugar = sugar;
        Salt = salt;
        CreatedBy = createdBy;
    }
}
