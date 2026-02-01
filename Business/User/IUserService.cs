using NutriCore.Models;

namespace NutriCore.Business;

public interface IUserService
{
    User RegisterUser(UserCreateUpdateDto dto);
    IEnumerable<User> GetUsers();
    User GetUserById(int userId);
    User GetUserByEmail(string email);
    void UpdateUser(int userId, UserCreateUpdateDto dto);
    void DeleteUser(int userId);
}
