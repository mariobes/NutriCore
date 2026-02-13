using NutriCore.Models;
using System.Text.Json;

namespace NutriCore.Data;

public class UserJsonRepository : IUserRepository
{
    private Dictionary<string, User> _users = new Dictionary<string, User>();
    private readonly string _filePath;
    private static int UserIdSeed { get; set; }

    public UserJsonRepository()
    {
        // var basePath = AppDomain.CurrentDomain.BaseDirectory;
        // _filePath = Path.Combine(basePath, "JsonData", "Users.json");

        var basePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "JsonData");

        if (!Directory.Exists(basePath))
            Directory.CreateDirectory(basePath);

        _filePath = Path.Combine(basePath, "Users.json");

        // si el archivo no existe, crea uno vacío
        if (!File.Exists(_filePath))
            File.WriteAllText(_filePath, "[]");

        Console.WriteLine($"UserJsonRepository initialized with file path: {_filePath}");

        if (File.Exists(_filePath))
        {
            try
            {
                string jsonString = File.ReadAllText(_filePath);
                var users = JsonSerializer.Deserialize<IEnumerable<User>>(jsonString);
                _users = users.ToDictionary(acc => acc.Id.ToString());
            }
            catch (Exception e)
            {
                throw new Exception("An error occurred while reading the users file", e);
            }
        }

        UserIdSeed = _users.Any() ? _users.Values.Max(u => u.Id) + 1 : 1;
    }

    public void AddEntity(User entity)
    {
        entity.Id = UserIdSeed++;
        _users[entity.Id.ToString()] = entity;
        SaveChanges();
    }

    public IEnumerable<User> GetEntities()
    {
        return _users.Values;
    }

    public User GetEntityById(int entityId) => _users.FirstOrDefault(u => u.Value.Id == entityId).Value;

    public User GetUserByEmail(string email) => _users.FirstOrDefault(u => u.Value.Email == email).Value;

    public void UpdateEntity(User entity)
    {
        _users[entity.Id.ToString()] = entity;
        SaveChanges();
    }

    public void DeleteEntity(User entity)
    {
        _users.Remove(entity.Id.ToString());
        SaveChanges();
    }

    public void SaveChanges()
    {
        try
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(_users.Values, options);
            File.WriteAllText(_filePath, jsonString);
        }
        catch (Exception e)
        {
            throw new Exception("An error occurred while saving changes to the users file", e);
        }
    }
}