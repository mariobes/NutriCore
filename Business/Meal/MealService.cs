using NutriCore.Data;
using NutriCore.Models;

namespace NutriCore.Business;

public class MealService : IMealService
{
    private readonly IMealRepository _mealRepository;
    private readonly IFoodRepository _foodRepository;

    public MealService(IMealRepository mealRepository, IFoodRepository foodRepository)
    {
        _mealRepository = mealRepository;
        _foodRepository = foodRepository;
    }

    public Meal RegisterMeal(MealCreateUpdateDto dto)
    {
        var normalizedName = dto.Name.Trim().ToLower();
        var registeredName = _mealRepository.GetEntities().FirstOrDefault(m => m.Name.ToLower() == normalizedName);
        if (registeredName != null)
        {
            throw new Exception("The name is already registered.");
        }

        var ingredients = dto.Ingredients.Select(i => new MealIngredient
        {
            FoodId = i.FoodId,
            Food = _foodRepository.GetEntityById(i.FoodId),
            Quantity = i.Quantity
        }).ToList();

        Meal meal = new Meal
        {
            UserId = dto.UserId,
            Name = dto.Name,
            Image = dto.Image,
            Ingredients = ingredients
        };
        _mealRepository.AddEntity(meal);
        return meal;
    }

    public IEnumerable<Meal> GetMeals()
    {
        return _mealRepository.GetEntities();
    }

    public IEnumerable<Meal> GetMealsByUser(int userId)
    {
        return _mealRepository.GetMealsByUser(userId);
    }

    public Meal GetMealById(int mealId, int userId)
    {
        var meal = _mealRepository.GetEntityById(mealId);
        if (meal == null)
        {
            throw new KeyNotFoundException($"Meal with ID {mealId} not found.");
        }

        if (userId != 1 && meal.UserId != userId && meal.UserId != 1)
        {
            throw new UnauthorizedAccessException("You can only get your own meals.");
        }

        return meal;
    }

    public void UpdateMeal(int mealId, MealCreateUpdateDto dto)
    {
        var meal = _mealRepository.GetEntityById(mealId);
        if (meal == null)
        {
            throw new KeyNotFoundException($"Meal with ID {mealId} not found.");
        }

        if (dto.UserId != 1 && meal.UserId != dto.UserId)
        {
            throw new UnauthorizedAccessException("You can only update your own meals.");
        }

        if (!string.IsNullOrWhiteSpace(dto.Name))
        {
            var normalizedName = dto.Name.Trim().ToLower();
            if (!string.Equals(meal.Name, normalizedName, StringComparison.OrdinalIgnoreCase))
            {
                var registeredName = _mealRepository.GetEntities().FirstOrDefault(m => m.Name.ToLower() == normalizedName);
                if (registeredName != null)
                {
                    throw new Exception("The name is already registered.");
                }

                meal.Name = normalizedName;
            }
        }
        
        if (dto.Ingredients != null && dto.Ingredients.Any())
        {
            var ingredients = dto.Ingredients.Select(i =>
            {
                var food = _foodRepository.GetEntityById(i.FoodId);
                if (food == null)
                    throw new Exception($"Food with ID {i.FoodId} not found.");

                return new MealIngredient
                {
                    MealId = meal.Id,
                    FoodId = food.Id,
                    Food = food,
                    Quantity = i.Quantity
                };
            }).ToList();

            meal.Ingredients = ingredients;
        }

        meal.UserId = dto.UserId;
        meal.Image = dto.Image;
        _mealRepository.UpdateEntity(meal);
    }

    public void DeleteMeal(int mealId, int userId)
    {
        var meal = _mealRepository.GetEntityById(mealId);
        if (meal == null)
        {
            throw new KeyNotFoundException($"Meal with ID {mealId} not found.");
        }

        if (userId != 1 && meal.UserId != userId)
        {
            throw new UnauthorizedAccessException("You can only delete your own meals.");
        }

        _mealRepository.DeleteEntity(meal);
    }
}