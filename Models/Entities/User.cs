using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;

namespace NutriCore.Models;

public class User
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string? Name { get; set; }

    [Required]
    public string? Email { get; set; }

    [Required]
    public string? Password { get; set; }

    [Required]
    public int Age { get; set; }

    [Required]
    public int Height { get; set; }

    [Required]
    public int Weight { get; set; }

    [Required]
    public string? Country { get; set; }

    [Required]
    public string Role { get; set; } = Roles.User;

    public User() {}

    public User(string name, string email, string password, int age, int height, int weight, string country) 
    {
        Name = name;
        Email = email;
        Password = password;
        Age = age;
        Height = height;
        Weight = weight;
        Country = country;
    }

    public static class PasswordHasher
    {
        public static string Hash(string? password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }

        public static bool Verify(string inputPassword, string storedHashedPassword)
        {
            var hashedInput = Hash(inputPassword);
            return hashedInput == storedHashedPassword;
        }
    }
}
