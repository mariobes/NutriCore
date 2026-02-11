using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace NutriCore.Models;

public class Intake
{
    [Key]
    public int Id { get; set; }

    [Required]
    [ForeignKey("User")]
    public int UserId { get; set; }

    [Required]
    public int ConsumableId { get; set; }

    [Required]
    public string? ConsumableType { get; set; }

    [Required]
    public DateTime Date { get; set; }

    public int? FoodQuantity { get; set; }

    [Required]
    public int? TotalKilocalories { get; set; }

    [Required]
    public double? TotalFats { get; set; }

    [Required]
    public double? TotalCarbohydrates { get; set; }

    [Required]
    public double? TotalProteins { get; set; }

    [Required]
    public double? TotalFiber { get; set; }

    [Required]
    public double? TotalSugar { get; set; }

    [Required]
    public double? TotalSalt { get; set; }

    [JsonIgnore]
    public User? User { get; set; }

    [JsonIgnore]
    public Food? Food { get; set; }

    [JsonIgnore]
    public Meal? Meal { get; set; }
}