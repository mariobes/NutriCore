using NutriCore.Data;
using NutriCore.Models;

namespace NutriCore.Business;

public class FoodService : IFoodService
{
    private readonly IFoodRepository _repository;

    public FoodService(IFoodRepository repository)
    {
        _repository = repository;
    }

    public Food RegisterFood(FoodCreateUpdateDto dto)
    {
        var normalizedName = dto.Name.Trim().ToLower();
        var registeredName = _repository.GetEntities().FirstOrDefault(f => f.Name.ToLower() == normalizedName && f.CreatedBy == "user");
        if (registeredName != null)
        {
            throw new Exception("The name is already registered.");
        }

        Food food = new Food
        {
            UserId = dto.UserId,
            Name = dto.Name.Trim(),
            Image = dto.Image,
            UnitOfMeasurement = dto.UnitOfMeasurement,
            MeasurementQuantity = dto.MeasurementQuantity,
            Kilocalories = dto.Kilocalories,
            Fats = dto.Fats,
            Carbohydrates = dto.Carbohydrates,
            Proteins = dto.Proteins,
            Fiber = dto.Fiber,
            Sugar = dto.Sugar,
            Salt = dto.Salt
        };
        _repository.AddEntity(food);
        return food;
    }

    public IEnumerable<Food> GetFoods()
    {
        return _repository.GetEntities();
    }

    public IEnumerable<Food> GetFoodsByUser(int userId)
    {
        return _repository.GetFoodsByUser(userId);
    }

    public Food GetFoodById(int foodId, int userId)
    {
        var food = _repository.GetEntityById(foodId);
        if (food == null)
        {
            throw new KeyNotFoundException($"Food with ID {foodId} not found.");
        }

        if (userId != 1 && food.UserId != userId && food.UserId != 1)
        {
            throw new UnauthorizedAccessException("You can only get your own foods.");
        }

        return food;
    }

    public void UpdateFood(int foodId, FoodCreateUpdateDto dto)
    {
        var food = _repository.GetEntityById(foodId);
        if (food == null)
        {
            throw new KeyNotFoundException($"Food with ID {foodId} not found.");
        }

        if (dto.UserId != 1 && food.UserId != dto.UserId)
        {
            throw new UnauthorizedAccessException("You can only update your own foods.");
        }

        if (!string.IsNullOrWhiteSpace(dto.Name))
        {
            var normalizedName = dto.Name.Trim().ToLower();
            if (!string.Equals(food.Name, normalizedName, StringComparison.OrdinalIgnoreCase))
            {
                var registeredName = _repository.GetEntities().FirstOrDefault(f => f.Name.ToLower() == normalizedName && f.CreatedBy == "user");
                if (registeredName != null)
                {
                    throw new Exception("The name is already registered.");
                }

                food.Name = dto.Name.Trim();   
            }
        }

        food.UserId = dto.UserId;
        food.Image = dto.Image;
        food.UnitOfMeasurement = dto.UnitOfMeasurement;
        food.MeasurementQuantity = dto.MeasurementQuantity;
        food.Kilocalories = dto.Kilocalories;
        food.Fats = dto.Fats;
        food.Carbohydrates = dto.Carbohydrates;
        food.Proteins = dto.Proteins;
        food.Fiber = dto.Fiber;
        food.Sugar = dto.Sugar;
        food.Salt = dto.Salt;
        _repository.UpdateEntity(food);
    }

    public void DeleteFood(int foodId, int userId)
    {
        var food = _repository.GetEntityById(foodId);
        if (food == null)
        {
            throw new KeyNotFoundException($"Food with ID {foodId} not found.");
        }

        if (userId != 1 && food.UserId != userId)
        {
            throw new UnauthorizedAccessException("You can only delete your own foods.");
        }

        _repository.DeleteEntity(food);
    }

    public List<Food> SearchFood(string query, int userId)
    {
        var defaultFoods = 
            _repository.GetFoodsByUser(1)
                .Where(f => f.Name.Contains(query, StringComparison.OrdinalIgnoreCase))
                .OrderByDescending(f => f.Id)
                .ToList();

        var userFoods = 
            _repository.GetFoodsByUser(userId)
                .Where(f => f.Name.Contains(query, StringComparison.OrdinalIgnoreCase))
                .OrderByDescending(f => f.Id)
                .ToList();

        return userFoods.Concat(defaultFoods).ToList();
    }
}