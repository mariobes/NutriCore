using NutriCore.Models;
using Microsoft.EntityFrameworkCore;

namespace NutriCore.Data;

public class UserEFRepository : IGenericRepository<User>
{
    private readonly NutriCoreContext _context;

    public UserEFRepository(NutriCoreContext context)
    {
        _context = context;
    }

    public void AddEntity(User entity)
    {
        _context.Users.Add(entity);
        SaveChanges();
    }

    public IEnumerable<User> GetAllEntities()
    {
        return _context.Users.ToList();
    }

    public User? GetEntityById(int entityId)
    {
        return _context.Users.FirstOrDefault(u => u.Id == entityId);
    }

    public User? GetEntityByEmail(string email)
    {
        return _context.Users.FirstOrDefault(u => u.Email == email);
    }

    public void UpdateEntity(User user)
    {
        _context.Entry(user).State = EntityState.Modified;
        SaveChanges();
    }

    public void DeleteEntity(User entity) 
    {
        _context.Users.Remove(entity);
        SaveChanges();
    }
    
    public void SaveChanges()
    {
        _context.SaveChanges();
    }
}