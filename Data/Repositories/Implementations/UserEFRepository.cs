using NutriCore.Models;
using Microsoft.EntityFrameworkCore;

namespace NutriCore.Data;

public class UserEFRepository : IUserRepository
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

    public IEnumerable<User> GetEntities()
    {
        return _context.Users.ToList();
    }

    public User? GetEntityById(int entityId)
    {
        return _context.Users.FirstOrDefault(u => u.Id == entityId);
    }

    public User? GetUserByEmail(string email)
    {
        return _context.Users.FirstOrDefault(u => u.Email == email);
    }

    public void UpdateEntity(User entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
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