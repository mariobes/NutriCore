using System.ComponentModel.DataAnnotations;

namespace NutriCore.Models;

public class UserTargetsDto
{
    [Range(0, 10000, ErrorMessage = "Daily kilocalories must be between 0 and 10000")]
    public double? DailyKilocalorieTarget { get; set; }

    [Range(0, 1000, ErrorMessage = "Daily fat must be between 0 and 1000 grams")]
    public double? DailyFatTarget { get; set; }

    [Range(0, 1500, ErrorMessage = "Daily carbohydrates must be between 0 and 1500 grams")]
    public double? DailyCarbohydrateTarget { get; set; }

    [Range(0, 1000, ErrorMessage = "Daily protein must be between 0 and 1000 grams")]
    public double? DailyProteinTarget { get; set; }

    [Range(0, 6, ErrorMessage = "Daily water must be between 0 and 6 liters")]
    public double? DailyWaterTarget { get; set; }
}