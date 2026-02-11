using NutriCore.Data;
using NutriCore.Models;

namespace NutriCore.Business;

public class IntakeService : IIntakeService
{
    private readonly IIntakeRepository _intakeRepository;
    private readonly IUserRepository _userRepository;
    private readonly IFoodRepository _foodRepository;

    public IntakeService(IIntakeRepository intakeRepository, IUserRepository userRepository, IFoodRepository foodRepository)
    {
        _intakeRepository = intakeRepository;
        _userRepository = userRepository;
        _foodRepository = foodRepository;
    }

    public Intake RegisterIntake(IntakeCreateUpdateDto dto)
    {
        var user = _userRepository.GetEntityById(dto.UserId);
        if (user == null)
        {
            throw new KeyNotFoundException($"User with ID {dto.UserId} not found.");
        }

        Intake intake = new Intake
        {
            UserId = dto.UserId,
            ConsumableId = dto.ConsumableId,
            ConsumableType = dto.ConsumableType,
            Date = DateTime.Now
        };

        if (dto.ConsumableType == "food")
        {
            var food = _foodRepository.GetEntityById(dto.ConsumableId);
            if (food == null)
            {
                throw new KeyNotFoundException($"Food with ID {dto.ConsumableId} not found.");
            }

            if (dto.FoodQuantity == null || dto.FoodQuantity <= 0)
            {
                throw new Exception("FoodQuantity must be greater than 0.");
            }
  
            if (food.MeasurementQuantity <= 0)
            {
                throw new Exception($"Food {food.Name} has invalid MeasurementQuantity.");
            }
                
            double factor = (double)dto.FoodQuantity.Value / food.MeasurementQuantity;

            intake.FoodQuantity = dto.FoodQuantity;

            intake.TotalKilocalories = (int)Math.Round(food.Kilocalories * factor);
            intake.TotalFats = Math.Round(food.Fats!.Value * factor, 2);
            intake.TotalCarbohydrates = Math.Round(food.Carbohydrates!.Value * factor, 2);
            intake.TotalProteins = Math.Round(food.Proteins!.Value * factor, 2);
            intake.TotalFiber = Math.Round(food.Fiber!.Value * factor, 2);
            intake.TotalSugar = Math.Round(food.Sugar!.Value * factor, 2);
            intake.TotalSalt = Math.Round(food.Salt!.Value * factor, 2);
        }
        else
        {
            intake.TotalKilocalories = dto.TotalKilocalories;
            intake.TotalFats = dto.TotalFats;
            intake.TotalCarbohydrates = dto.TotalCarbohydrates;
            intake.TotalProteins = dto.TotalProteins;
            intake.TotalFiber = dto.TotalFiber;
            intake.TotalSugar = dto.TotalSugar;
            intake.TotalSalt = dto.TotalSalt;
        }

        _intakeRepository.AddEntity(intake);
        return intake;
    }

    public IEnumerable<Intake> GetIntakes()
    {
        return _intakeRepository.GetEntities();
    }

    public IEnumerable<Intake> GetIntakesByUser(int userId)
    {
        return _intakeRepository.GetIntakesByUser(userId);
    }

    public Intake GetIntakeById(int intakeId)
    {
        var intake = _intakeRepository.GetEntityById(intakeId);
        if (intake == null)
        {
            throw new KeyNotFoundException($"Intake with ID {intakeId} not found.");
        }
        return intake;
    }

    public void UpdateIntake(int intakeId, IntakeCreateUpdateDto dto)
    {
        var intake = _intakeRepository.GetEntityById(intakeId);
        if (intake == null)
        {
            throw new KeyNotFoundException($"Intake with ID {intakeId} not found.");
        }

        intake.UserId = dto.UserId;
        intake.ConsumableId = dto.ConsumableId;
        intake.ConsumableType = dto.ConsumableType;
        intake.Date = DateTime.Now;

        if (dto.ConsumableType == "food")
        {
            var food = _foodRepository.GetEntityById(dto.ConsumableId);
            if (food == null)
            {
                throw new KeyNotFoundException($"Food with ID {dto.ConsumableId} not found.");
            }

            if (dto.FoodQuantity == null || dto.FoodQuantity <= 0)
            {
                throw new Exception("FoodQuantity must be greater than 0.");
            }
  
            if (food.MeasurementQuantity <= 0)
            {
                throw new Exception($"Food {food.Name} has invalid MeasurementQuantity.");
            }

            var factor = dto.FoodQuantity.Value / food.MeasurementQuantity;

            intake.FoodQuantity = dto.FoodQuantity;
            intake.TotalKilocalories = (int)Math.Round((double)food.Kilocalories * factor);
            intake.TotalFats = Math.Round(food.Fats!.Value * factor, 2);
            intake.TotalCarbohydrates = Math.Round(food.Carbohydrates!.Value * factor, 2);
            intake.TotalProteins = Math.Round(food.Proteins!.Value * factor, 2);
            intake.TotalFiber = Math.Round(food.Fiber!.Value * factor, 2);
            intake.TotalSugar = Math.Round(food.Sugar!.Value * factor, 2);
            intake.TotalSalt = Math.Round(food.Salt!.Value * factor, 2);
        }
        else
        {
            intake.TotalKilocalories = dto.TotalKilocalories;
            intake.TotalFats = dto.TotalFats;
            intake.TotalCarbohydrates = dto.TotalCarbohydrates;
            intake.TotalProteins = dto.TotalProteins;
            intake.TotalFiber = dto.TotalFiber;
            intake.TotalSugar = dto.TotalSugar;
            intake.TotalSalt = dto.TotalSalt;
        }

        _intakeRepository.UpdateEntity(intake);
    }

    public void DeleteIntake(int intakeId)
    {
        var intake = _intakeRepository.GetEntityById(intakeId);
        if (intake == null)
        {
            throw new KeyNotFoundException($"Intake with ID {intakeId} not found.");
        }
        _intakeRepository.DeleteEntity(intake);
    }
}