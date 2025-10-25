using NutriCore.Models;

namespace NutriCore.Data;

public interface IUserRepository : IGenericRepository<User>
{
    User? GetUserByEmail(string email);
}