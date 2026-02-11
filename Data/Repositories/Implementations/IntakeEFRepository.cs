using NutriCore.Models;
using Microsoft.EntityFrameworkCore;

namespace NutriCore.Data;

public class IntakeEFRepository : IIntakeRepository
{
    private readonly NutriCoreContext _context;

    public IntakeEFRepository(NutriCoreContext context)
    {
        _context = context;
    }

    public void AddEntity(Intake entity)
    {
        _context.Intakes.Add(entity);
        SaveChanges();
    }

    public IEnumerable<Intake> GetEntities()
    {
        return _context.Intakes.ToList();
    }

    public IEnumerable<Intake> GetIntakesByUser(int userId)
    {
        return _context.Intakes.Where(i => i.UserId == userId).ToList();
    }

    public Intake? GetEntityById(int entityId)
    {
        return _context.Intakes.FirstOrDefault(i => i.Id == entityId);
    }

    public void UpdateEntity(Intake entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        SaveChanges();
    }

    public void DeleteEntity(Intake entity) 
    {
        _context.Intakes.Remove(entity);
        SaveChanges();
    }

    public void SaveChanges()
    {
        _context.SaveChanges();
    }
}