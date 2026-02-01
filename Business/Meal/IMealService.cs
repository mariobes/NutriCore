using NutriCore.Models;

namespace NutriCore.Business;

public interface IMealService
{
    Meal RegisterMeal(MealCreateUpdateDto dto);
    IEnumerable<Meal> GetMeals();
    IEnumerable<Meal> GetMealsByUser(int userId);
    Meal GetMealById(int mealId, int userId);
    void UpdateMeal(int mealId, MealCreateUpdateDto dto);
    void DeleteMeal(int mealId, int userId);
}
