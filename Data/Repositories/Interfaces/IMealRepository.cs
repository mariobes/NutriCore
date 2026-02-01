using NutriCore.Models;

namespace NutriCore.Data;

public interface IMealRepository : IGenericRepository<Meal>
{
    IEnumerable<Meal> GetMealsByUser(int userId);
}