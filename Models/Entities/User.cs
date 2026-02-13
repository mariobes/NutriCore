using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

namespace NutriCore.Models;

public class User // Ideal quitar required y ? para poner = ""; (Pero creo que esto no me creará los datos del context)
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
    public double DailyWater { get; set; }

    [Required]
    public DateTime DateDailyWater { get; set; }

    [Required]
    public double DailyKilocalorieTarget { get; set; }

    [Required]
    public double DailyFatTarget { get; set; }

    [Required]
    public double DailyCarbohydrateTarget { get; set; }

    [Required]
    public double DailyProteinTarget { get; set; }

    [Required]
    public double DailyWaterTarget { get; set; }

    public string Role { get; set; } = Roles.User;

    public static class PasswordHasher
    {
        private const int SaltSize = 16;
        private const int KeySize = 32;
        private const int Iterations = 100_000;

        public static string Hash(string password)
        {
            using var rng = RandomNumberGenerator.Create();
            byte[] salt = new byte[SaltSize];
            rng.GetBytes(salt);

            using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256);
            byte[] key = pbkdf2.GetBytes(KeySize);

            return $"{Convert.ToBase64String(salt)}.{Convert.ToBase64String(key)}";
        }

        public static bool Verify(string password, string storedHash)
        {
            var parts = storedHash.Split('.', 2);
            if (parts.Length != 2)
                return false;

            byte[] salt = Convert.FromBase64String(parts[0]);
            byte[] key = Convert.FromBase64String(parts[1]);

            using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256);
            byte[] keyToCheck = pbkdf2.GetBytes(KeySize);

            return CryptographicOperations.FixedTimeEquals(key, keyToCheck);
        }
    }
}