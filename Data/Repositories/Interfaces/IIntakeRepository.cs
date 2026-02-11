using NutriCore.Models;

namespace NutriCore.Data;

public interface IIntakeRepository : IGenericRepository<Intake>
{
    IEnumerable<Intake> GetIntakesByUser(int userId);
}