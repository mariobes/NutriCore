using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NutriCore.Models;

public class Meal
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
    public List<MealIngredient> Ingredients { get; set; } = new List<MealIngredient>();

    [Required]
    public int TotalKilocalories { get; set; }

    [Required]
    public double TotalFats { get; set; }

    [Required]
    public double TotalCarbohydrates { get; set; }

    [Required]
    public double TotalProteins { get; set; }

    [Required]
    public double TotalFiber { get; set; }

    [Required]
    public double TotalSugar { get; set; }

    [Required]
    public double TotalSalt { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? CreatedBy { get; set; } = Roles.User;
}