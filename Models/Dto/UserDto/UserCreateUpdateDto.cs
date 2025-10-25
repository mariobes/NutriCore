using System.ComponentModel.DataAnnotations;

namespace NutriCore.Models;

public class UserCreateUpdateDto
{
    [Required(ErrorMessage = "Name is required")]
    [StringLength(50, ErrorMessage = "Name must be less than 30 characters")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [RegularExpression(
        @"^(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*(),.?\""{}|<>])[A-Za-z\d!@#$%^&*(),.?\""{}|<>]{8,}$", 
        ErrorMessage = "Password must be at least 8 characters long, contain 1 uppercase letter, 1 number, and 1 special character")]
    public string? Password { get; set; }

    [Required(ErrorMessage = "Age is required")]
    [Range(1, 120, ErrorMessage = "Age must be between 1 and 120")]
    public int Age { get; set; }

    [Required(ErrorMessage = "Height is required")]
    [Range(30, 250, ErrorMessage = "Height must be between 30 cm and 250 cm")]
    public int Height { get; set; }

    [Required(ErrorMessage = "Weight is required")]
    [Range(1, 500, ErrorMessage = "Weight must be between 1 kg and 500 kg")]
    public int Weight { get; set; }

    [Required(ErrorMessage = "Country is required")]
    public string? Country { get; set; }
}