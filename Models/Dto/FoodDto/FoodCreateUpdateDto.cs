using System.ComponentModel.DataAnnotations;

namespace NutriCore.Models;

public class FoodCreateUpdateDto
{
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "User ID must be greater than 0")]
    public int UserId { get; set; }

    [Required(ErrorMessage = "Name is required")]
    [StringLength(100, ErrorMessage = "Name must be less than 30 characters")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "Image URL is required")]
    [Url(ErrorMessage = "Image must be a valid URL")]
    public string? Image { get; set; }

    [Required(ErrorMessage = "Unit of measurement is required")]
    public EnumUnitOfMeasurementOptions? UnitOfMeasurement { get; set; }

    [Required(ErrorMessage = "Measurement quantity is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Measurement quantity must be greater than 0")]
    public int MeasurementQuantity { get; set; }

    [Required(ErrorMessage = "Kilocalories is required")]
    [Range(0, int.MaxValue, ErrorMessage = "Kilocalories cannot be negative")]
    public int Kilocalories { get; set; }

    [Required(ErrorMessage = "Fats is required")]
    [Range(0, int.MaxValue, ErrorMessage = "Fats cannot be negative")]
    public double Fats { get; set; }

    [Required(ErrorMessage = "Carbohydrates is required")]
    [Range(0, int.MaxValue, ErrorMessage = "Carbohydrates cannot be negative")]
    public double Carbohydrates { get; set; }

    [Required(ErrorMessage = "Proteins is required")]
    [Range(0, int.MaxValue, ErrorMessage = "Proteins cannot be negative")]
    public double Proteins { get; set; }

    [Required(ErrorMessage = "Sugar is required")]
    [Range(0, int.MaxValue, ErrorMessage = "Sugar cannot be negative")]
    public double Sugar { get; set; }

    [Required(ErrorMessage = "Salt is required")]
    [Range(0, int.MaxValue, ErrorMessage = "Salt cannot be negative")]
    public double Salt { get; set; }
}