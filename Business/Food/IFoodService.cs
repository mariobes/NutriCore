using NutriCore.Models;

namespace NutriCore.Business;

public interface IFoodService
{
    Food RegisterFood(FoodCreateUpdateDto dto);
    IEnumerable<Food> GetFoods();
    IEnumerable<Food> GetFoodsByUser(int userId);
    Food GetFoodById(int foodId, int userId);
    void UpdateFood(int foodId, FoodCreateUpdateDto dto);
    void DeleteFood(int foodId, int userId);
}
