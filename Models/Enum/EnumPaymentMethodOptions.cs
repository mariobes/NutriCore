using System.ComponentModel.DataAnnotations;

public enum EnumUnitOfMeasurementOptions
{
    [Display(Name = "g")]
    Grams = 0,

    [Display(Name = "ml")]
    Milliliters = 1,

    [Display(Name = "l")]
    Liters = 2
}