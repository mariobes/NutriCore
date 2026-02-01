using NutriCore.Models;
using System.Text.Json;

namespace NutriCore.Data;

public class MealJsonRepository : IMealRepository
{
    private Dictionary<string, Meal> _meals = new Dictionary<string, Meal>();
    private Dictionary<string, Food> _foods = new Dictionary<string, Food>();
    private readonly string _mealPath;
    private readonly string _foodPath;
    private static int MealIdSeed { get; set; }

    public MealJsonRepository()
    {
        var basePath = AppDomain.CurrentDomain.BaseDirectory;
        _mealPath = Path.Combine(basePath, "Json", "Data", "Meals.json");
        _foodPath = Path.Combine(basePath, "Json", "Data", "Foods.json");

        if (File.Exists(_mealPath))
        {
            try
            {
                string jsonString = File.ReadAllText(_mealPath);
                var meals = JsonSerializer.Deserialize<IEnumerable<Meal>>(jsonString);
                _meals = meals.ToDictionary(acc => acc.Id.ToString());
            }
            catch (Exception e)
            {
                throw new Exception("An error occurred while reading the meals file", e);
            }
        }

        if (File.Exists(_foodPath))
        {
            try
            {
                string jsonString = File.ReadAllText(_foodPath);
                var foods = JsonSerializer.Deserialize<IEnumerable<Food>>(jsonString);
                _foods = foods.ToDictionary(acc => acc.Id.ToString());
            }
            catch (Exception e)
            {
                throw new Exception("An error occurred while reading the foods file", e);
            }
        }

        MealIdSeed = _meals.Any() ? _meals.Values.Max(u => u.Id) + 1 : 1;
    }

    public void AddEntity(Meal entity)
    {
        entity.Id = MealIdSeed++;
        _meals[entity.Id.ToString()] = entity;
        SaveChanges();
    }

    public IEnumerable<Meal> GetEntities()
    {
        return _meals.Values;
    }

    public IEnumerable<Meal> GetMealsByUser(int userId)
    {
        var meals = _meals.Values.Where(m => m.UserId == userId).ToList();

        foreach (var meal in meals)
        {
            foreach (var ingredient in meal.Ingredients)
            {
                if (_foods.TryGetValue(ingredient.FoodId.ToString(), out var food))
                {
                    ingredient.Food = food;
                }
            }
        }

        return meals;
    }

    public Meal GetEntityById(int entityId) => _meals.FirstOrDefault(m => m.Value.Id == entityId).Value;

    public void UpdateEntity(Meal entity)
    {
        _meals[entity.Id.ToString()] = entity;
        SaveChanges();
    }

    public void DeleteEntity(Meal entity)
    {
        _meals.Remove(entity.Id.ToString());
        SaveChanges();
    }

    public void SaveChanges()
    {
        try
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(_meals.Values, options);
            File.WriteAllText(_mealPath, jsonString);
        }
        catch (Exception e)
        {
            throw new Exception("An error occurred while saving changes to the meals file", e);
        }
    }
}