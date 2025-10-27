using NutriCore.Models;
using Microsoft.EntityFrameworkCore;

namespace NutriCore.Data;

public class MealEFRepository : IMealRepository
{
    private readonly NutriCoreContext _context;

    public MealEFRepository(NutriCoreContext context)
    {
        _context = context;
    }

    public void AddEntity(Meal entity)
    {
        _context.Meals.Add(entity);
        SaveChanges();
    }

    public IEnumerable<Meal> GetAllEntities()
    {
        return _context.Meals.Include(m => m.Ingredients).ThenInclude(mi => mi.Food).ToList();
    }

    public IEnumerable<Meal> GetAllMealsByUser(int userId)
    {
        return _context.Meals.Where(m => m.UserId == userId).Include(m => m.Ingredients).ThenInclude(mi => mi.Food).ToList();
    }

    public Meal? GetEntityById(int entityId)
    {
        return _context.Meals.Include(m => m.Ingredients).ThenInclude(mi => mi.Food).FirstOrDefault(m => m.Id == entityId);
    }

    public void UpdateEntity(Meal meal)
    {
        _context.Entry(meal).State = EntityState.Modified;
        SaveChanges();
    }

    public void DeleteEntity(Meal entity)
    {
        _context.Meals.Remove(entity);
        SaveChanges();
    }

    public void SaveChanges()
    {
        _context.SaveChanges();
    }
}