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
    public double? Fats { get; set; }

    [Required]
    public double? Carbohydrates { get; set; }

    [Required]
    public double? Proteins { get; set; }

    [Required]
    public double? Fiber { get; set; }

    [Required]
    public double? Sugar { get; set; }

    [Required]
    public double? Salt { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? CreatedBy { get; set; } = Roles.User;
}