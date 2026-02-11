using System.ComponentModel.DataAnnotations;

namespace NutriCore.Models;

public class IntakeCreateUpdateDto
{
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "User ID must be greater than 0")]
    public int UserId { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Consumable ID must be greater than 0")]
    public int ConsumableId { get; set; }

    [Required(ErrorMessage = "Name is required")]
    [RegularExpression("^(food|meal)$", ErrorMessage = "ConsumableType must be 'food' or 'meal'")]
    public string? ConsumableType { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Food quantity must be greater than 0")]
    public int? FoodQuantity { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Total kilocalories cannot be negative")]
    public int? TotalKilocalories { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Total fats cannot be negative")]
    public double? TotalFats { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Total carbohydrates cannot be negative")]
    public double? TotalCarbohydrates { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Total proteins cannot be negative")]
    public double? TotalProteins { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Total fiber cannot be negative")]
    public double? TotalFiber { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Total sugar cannot be negative")]
    public double? TotalSugar { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Total salt cannot be negative")]
    public double? TotalSalt { get; set; }
}