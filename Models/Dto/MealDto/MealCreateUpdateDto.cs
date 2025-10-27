using System.ComponentModel.DataAnnotations;

namespace NutriCore.Models;

public class MealCreateUpdateDto
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

    [Required(ErrorMessage = "At least one ingredient must be provided")]
    [MinLength(1, ErrorMessage = "At least one ingredient is required")]
    public List<MealIngredientCreateDto> Ingredients { get; set; } = new List<MealIngredientCreateDto>();
}