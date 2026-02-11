using NutriCore.Models;
using System.Text.Json;

namespace NutriCore.Data;

public class IntakeJsonRepository : IIntakeRepository
{
    private Dictionary<string, Intake> _intakes = new Dictionary<string, Intake>();
    private readonly string _filePath;
    private static int IntakeIdSeed { get; set; }

    public IntakeJsonRepository()
    {
        var basePath = AppDomain.CurrentDomain.BaseDirectory;
        _filePath = Path.Combine(basePath, "Json", "Data", "Intakes.json");

        if (File.Exists(_filePath))
        {
            try
            {
                string jsonString = File.ReadAllText(_filePath);
                var intakes = JsonSerializer.Deserialize<IEnumerable<Intake>>(jsonString);
                _intakes = intakes.ToDictionary(acc => acc.Id.ToString());
            }
            catch (Exception e)
            {
                throw new Exception("An error occurred while reading the users file", e);
            }
        }

        IntakeIdSeed = _intakes.Any() ? _intakes.Values.Max(u => u.Id) + 1 : 1;
    }

    public void AddEntity(Intake entity)
    {
        entity.Id = IntakeIdSeed++;
        _intakes[entity.Id.ToString()] = entity;
        SaveChanges();
    }

    public IEnumerable<Intake> GetEntities()
    {
        return _intakes.Values;
    }

    public IEnumerable<Intake> GetIntakesByUser(int userId) => _intakes.Values.Where(i => i.UserId == userId).ToList();

    public Intake GetEntityById(int entityId) => _intakes.FirstOrDefault(i => i.Value.Id == entityId).Value;

    public void UpdateEntity(Intake entity)
    {
        _intakes[entity.Id.ToString()] = entity;
        SaveChanges();
    }

    public void DeleteEntity(Intake entity)
    {
        _intakes.Remove(entity.Id.ToString());
        SaveChanges();
    }

    public void SaveChanges()
    {
        try
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(_intakes.Values, options);
            File.WriteAllText(_filePath, jsonString);
        }
        catch (Exception e)
        {
            throw new Exception("An error occurred while saving changes to the intakes file", e);
        }
    }
}