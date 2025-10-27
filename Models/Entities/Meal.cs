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

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? CreatedBy { get; set; } = "User";

    public Meal() {}

    public Meal(int userId, string name, string image, string createdBy)
    {
        UserId = userId;
        Name = name;
        Image = image;
        CreatedBy = createdBy;
    }
}