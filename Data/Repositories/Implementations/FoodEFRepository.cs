using NutriCore.Models;
using Microsoft.EntityFrameworkCore;

namespace NutriCore.Data;

public class FoodEFRepository : IFoodRepository
{
    private readonly NutriCoreContext _context;

    public FoodEFRepository(NutriCoreContext context)
    {
        _context = context;
    }

    public void AddEntity(Food entity)
    {
        _context.Foods.Add(entity);
        SaveChanges();
    }

    public IEnumerable<Food> GetAllEntities()
    {
        return _context.Foods.ToList();
    }

    public IEnumerable<Food> GetAllFoodsByUser(int userId)
    {
        return _context.Foods.Where(f => f.UserId == userId).ToList();
    }

    public Food? GetEntityById(int entityId)
    {
        return _context.Foods.FirstOrDefault(f => f.Id == entityId);
    }

    public void UpdateEntity(Food food)
    {
        _context.Entry(food).State = EntityState.Modified;
        SaveChanges();
    }

    public void DeleteEntity(Food entity)
    {
        _context.Foods.Remove(entity);
        SaveChanges();
    }

    public void SaveChanges()
    {
        _context.SaveChanges();
    }
}