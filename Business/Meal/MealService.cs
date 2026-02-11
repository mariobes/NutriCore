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
        var registeredName = _mealRepository.GetEntities().FirstOrDefault(m => m.Name.ToLower() == normalizedName && m.CreatedBy == "user");

        if (registeredName != null)
        {
            throw new Exception("The name is already registered.");
        }
            
        var ingredients = dto.Ingredients.Select(i =>
        {
            var food = _foodRepository.GetEntityById(i.FoodId);
            if (food == null)
            {
                throw new Exception($"Food with ID {i.FoodId} not found.");
            }

            return new MealIngredient
            {
                FoodId = food.Id,
                Food = food,
                Quantity = i.Quantity
            };
        }).ToList();

        var meal = new Meal
        {
            UserId = dto.UserId,
            Name = normalizedName,
            Image = dto.Image,
            Ingredients = ingredients
        };

        CalculateMealTotals(meal);

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
            throw new UnauthorizedAccessException("You can only update your own meals.");

        if (!string.IsNullOrWhiteSpace(dto.Name))
        {
            var normalizedName = dto.Name.Trim().ToLower();

            if (!string.Equals(meal.Name, normalizedName, StringComparison.OrdinalIgnoreCase))
            {
                var registeredName = _mealRepository.GetEntities().FirstOrDefault(m => m.Name.ToLower() == normalizedName && m.CreatedBy == "user");

                if (registeredName != null)
                    throw new Exception("The name is already registered.");

                meal.Name = normalizedName;
            }
        }

        if (dto.Ingredients != null)
        {
            meal.Ingredients = dto.Ingredients.Select(i =>
            {
                var food = _foodRepository.GetEntityById(i.FoodId);
                if (food == null)
                {
                    throw new Exception($"Food with ID {i.FoodId} not found.");
                }

                return new MealIngredient
                {
                    MealId = meal.Id,
                    FoodId = food.Id,
                    Food = food,
                    Quantity = i.Quantity
                };
            }).ToList();

            CalculateMealTotals(meal);
        }

        meal.Image = dto.Image;
        meal.UserId = dto.UserId;

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

    private void CalculateMealTotals(Meal meal)
    {
        double totalFats = 0;
        double totalCarbs = 0;
        double totalProteins = 0;
        double totalFiber = 0;
        double totalSugar = 0;
        double totalSalt = 0;
        int totalKcal = 0;

        foreach (var ingredient in meal.Ingredients)
        {
            var food = ingredient.Food 
                ?? throw new Exception("Ingredient food cannot be null");

            if (ingredient.Quantity == null || ingredient.Quantity <= 0)
                throw new Exception($"Invalid quantity for food {food.Name}");

            if (food.MeasurementQuantity <= 0)
                throw new Exception($"Food {food.Name} has invalid MeasurementQuantity");

            var factor = ingredient.Quantity.Value / food.MeasurementQuantity;

            totalKcal += (int)Math.Round(food.Kilocalories * factor);
            totalFats += food.Fats!.Value * factor;
            totalCarbs += food.Carbohydrates!.Value * factor;
            totalProteins += food.Proteins!.Value * factor;
            totalFiber += food.Fiber!.Value * factor;
            totalSugar += food.Sugar!.Value * factor;
            totalSalt += food.Salt!.Value * factor;
        }

        meal.TotalKilocalories = totalKcal;
        meal.TotalFats = Math.Round(totalFats, 2);
        meal.TotalCarbohydrates = Math.Round(totalCarbs, 2);
        meal.TotalProteins = Math.Round(totalProteins, 2);
        meal.TotalFiber = Math.Round(totalFiber, 2);
        meal.TotalSugar = Math.Round(totalSugar, 2);
        meal.TotalSalt = Math.Round(totalSalt, 2);
    }
}