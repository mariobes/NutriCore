using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NutriCore.Models;

public class MealIngredient
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int MealId { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Meal? Meal { get; set; }

    [Required]
    public int FoodId { get; set; }

    public Food? Food { get; set; }

    [Required]
    public double? Quantity { get; set; }
}