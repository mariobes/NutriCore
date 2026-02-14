using NutriCore.Models;
using System.Text.Json;

namespace NutriCore.Data;

public class FoodJsonRepository : IFoodRepository
{
    private Dictionary<string, Food> _foods = new Dictionary<string, Food>();
    private readonly string _filePath;
    private static int FoodIdSeed { get; set; }

    public FoodJsonRepository()
    {
        var basePath = AppDomain.CurrentDomain.BaseDirectory;
        _filePath = Path.Combine(basePath, "Json", "Data", "Foods.json");

        if (File.Exists(_filePath))
        {
            try
            {
                string jsonString = File.ReadAllText(_filePath);
                var foods = JsonSerializer.Deserialize<IEnumerable<Food>>(jsonString);
                _foods = foods.ToDictionary(acc => acc.Id.ToString());
            }
            catch (Exception e)
            {
                throw new Exception("An error occurred while reading the foods file", e);
            }
        }

        FoodIdSeed = _foods.Any() ? _foods.Values.Max(u => u.Id) + 1 : 1;
    }

    public void AddEntity(Food entity)
    {
        entity.Id = FoodIdSeed++;
        _foods[entity.Id.ToString()] = entity;
        SaveChanges();
    }

    public IEnumerable<Food> GetEntities()
    {
        return _foods.Values;
    }

    public IEnumerable<Food> GetFoodsByUser(int userId) => _foods.Values.Where(f => f.UserId == userId).ToList();

    public Food GetEntityById(int entityId) => _foods.FirstOrDefault(f => f.Value.Id == entityId).Value;

    public void UpdateEntity(Food entity)
    {
        _foods[entity.Id.ToString()] = entity;
        SaveChanges();
    }

    public void DeleteEntity(Food entity)
    {
        _foods.Remove(entity.Id.ToString());
        SaveChanges();
    }

    public void SaveChanges()
    {
        try
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(_foods.Values, options);
            File.WriteAllText(_filePath, jsonString);
        }
        catch (Exception e)
        {
            throw new Exception("An error occurred while saving changes to the foods file", e);
        }
    }
}