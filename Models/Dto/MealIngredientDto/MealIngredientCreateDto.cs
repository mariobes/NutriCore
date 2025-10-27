using System.ComponentModel.DataAnnotations;

namespace NutriCore.Models;

public class MealIngredientCreateDto
{
    [Required]
    public int FoodId { get; set; }

    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "Quantity must be greater than 0")]
    public double Quantity { get; set; }
}