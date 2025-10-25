using NutriCore.Models;

namespace NutriCore.Data;

public interface IFoodRepository : IGenericRepository<Food>
{
    IEnumerable<Food> GetAllFoodsByUser(int userId);
}