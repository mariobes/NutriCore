using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NutriCore.Models;

public class MealIngredient
{
    [Key]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public int Id { get; set; }

    [Required]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public int MealId { get; set; }

    [JsonIgnore]
    public Meal? Meal { get; set; }

    [Required]
    public int FoodId { get; set; }

    [JsonIgnore]
    public Food? Food { get; set; }

    [Required]
    public double? Quantity { get; set; }
}