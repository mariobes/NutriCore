using System.ComponentModel.DataAnnotations;

namespace NutriCore.Models;

public class UserLoginDto
{
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string? Email { get; set; }
    
    [Required(ErrorMessage = "Password is required")]
    [RegularExpression(
        @"^(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*(),.?\""{}|<>])[A-Za-z\d!@#$%^&*(),.?\""{}|<>]{8,}$", 
        ErrorMessage = "Password must be at least 8 characters long, contain 1 uppercase letter, 1 number, and 1 special character")]
    public string? Password { get; set; }
}