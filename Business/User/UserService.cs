using NutriCore.Data;
using NutriCore.Models;
using static NutriCore.Models.User;

namespace NutriCore.Business;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;

    public UserService(IUserRepository repository)
    {
        _repository = repository;
    }

    public User RegisterUser(UserCreateUpdateDto dto)
    {
        var normalizedEmail = dto.Email.Trim().ToLower();
        var registeredUserEmail = _repository.GetUserByEmail(normalizedEmail);
        if (registeredUserEmail != null)
        {
            throw new Exception("The email address is already registered.");
        }

        User user = new User
        {
            Name = dto.Name,
            Email = normalizedEmail,
            Password = PasswordHasher.Hash(dto.Password),
            Age = dto.Age,
            Height = dto.Height,
            Weight = dto.Weight,
            Country = dto.Country,
            DailyWater = 0,
            DailyKilocalorieTarget = 0,
            DailyFatTarget = 0,
            DailyCarbohydrateTarget = 0,
            DailyProteinTarget = 0
        };
        _repository.AddEntity(user);
        return user;
    }

    public IEnumerable<User> GetUsers()
    {
        return _repository.GetEntities();
    }

    public User GetUserById(int userId)
    {
        var user = _repository.GetEntityById(userId);
        if (user == null)
        {
            throw new KeyNotFoundException($"User with ID {userId} not found.");
        }
        return user;
    }

    public User GetUserByEmail(string email)
    {
        var user = _repository.GetUserByEmail(email);
        if (user == null)
        {
            throw new KeyNotFoundException($"User with email {email} not found.");
        }
        return user;
    }

    public void UpdateUser(int userId, UserCreateUpdateDto dto)
    {
        var user = _repository.GetEntityById(userId);
        if (user == null)
        {
            throw new KeyNotFoundException($"User with ID {userId} not found.");
        }

        if (!string.IsNullOrWhiteSpace(dto.Email))
        {
            var normalizedEmail = dto.Email.Trim().ToLower();
            if (!string.Equals(user.Email, normalizedEmail, StringComparison.OrdinalIgnoreCase))
            {
                var registeredUserEmail = _repository.GetUserByEmail(normalizedEmail);
                if (registeredUserEmail != null)
                {
                    throw new Exception("The email address is already registered.");
                }

                user.Email = normalizedEmail;   
            }
        }

        if (!string.IsNullOrWhiteSpace(dto.Password))
        {
            user.Password = PasswordHasher.Hash(dto.Password);
        }

        user.Name = dto.Name;
        user.Age = dto.Age;
        user.Height = dto.Height;
        user.Weight = dto.Weight;
        user.Country = dto.Country;
        _repository.UpdateEntity(user);
    }

    public void DeleteUser(int userId)
    {
        var user = _repository.GetEntityById(userId);
        if (user == null)
        {
            throw new KeyNotFoundException($"User with ID {userId} not found.");
        }
        _repository.DeleteEntity(user);
    }

    public void UpdateDailyWater(int userId, double dailyWater)
    {
        var user = _repository.GetEntityById(userId);
        if (user == null)
        {
            throw new KeyNotFoundException($"User with ID {userId} not found.");
        }

        if (dailyWater < 0 || dailyWater > 6)
        {
            throw new ArgumentOutOfRangeException("Daily water must be between 0 and 6 liters.");
        }

        user.DailyWater = dailyWater;
        _repository.UpdateEntity(user);
    }

    public UserTargetsDto GetTargets(int userId)
    {
        var user = _repository.GetEntityById(userId);
        if (user == null)
        {
            throw new KeyNotFoundException($"User with ID {userId} not found.");
        }

        var targets = new UserTargetsDto
        {
            DailyKilocalorieTarget = user.DailyKilocalorieTarget,
            DailyFatTarget = user.DailyFatTarget,
            DailyCarbohydrateTarget = user.DailyCarbohydrateTarget,
            DailyProteinTarget = user.DailyProteinTarget,
            DailyWaterTarget = user.DailyWaterTarget
        };

        return targets;
    }

    public void UpdateTargets(int userId, UserTargetsDto dto)
    {
        var user = _repository.GetEntityById(userId);
        if (user == null)
        {
            throw new KeyNotFoundException($"User with ID {userId} not found.");
        }

        if (dto.DailyKilocalorieTarget.HasValue)
        {
            user.DailyKilocalorieTarget = dto.DailyKilocalorieTarget.Value;
        }
        
        if (dto.DailyFatTarget.HasValue)
        {
            user.DailyFatTarget = dto.DailyFatTarget.Value;
        }

        if (dto.DailyCarbohydrateTarget.HasValue)
        {
            user.DailyCarbohydrateTarget = dto.DailyCarbohydrateTarget.Value;
        }

        if (dto.DailyProteinTarget.HasValue)
        {
            user.DailyProteinTarget = dto.DailyProteinTarget.Value;
        }

        if (dto.DailyWaterTarget.HasValue)
        {
            user.DailyWaterTarget = dto.DailyWaterTarget.Value;
        }

        _repository.UpdateEntity(user);
    }
}