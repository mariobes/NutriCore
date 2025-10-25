
namespace NutriCore.Data;

public interface IGenericRepository<T> where T : class
{
    void AddEntity(T entity);
    IEnumerable<T> GetAllEntities();
    T? GetEntityById(int entityId);
    void UpdateEntity(T entity);
    void DeleteEntity(T entity);
    void SaveChanges();
}