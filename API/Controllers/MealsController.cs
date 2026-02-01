using Microsoft.AspNetCore.Mvc;
using NutriCore.Business;
using NutriCore.Models;
using Microsoft.AspNetCore.Authorization;

namespace NutriCore.API.Controllers;

[ApiController]
[Route("users/{userId}/meals")]
public class MealsController : ControllerBase
{
    private readonly IMealService _mealService;
    private readonly IAuthService _authService;
    private readonly IUserService _userService;

    public MealsController(IMealService mealService, IAuthService authService, IUserService userService)
    {
        _mealService = mealService;
        _authService = authService;
        _userService = userService;
    }

    [Authorize(Roles = Roles.Admin + "," + Roles.User)]
    [HttpPost]
    public IActionResult CreateMeal([FromBody] MealCreateUpdateDto dto)
    {
        if (!ModelState.IsValid) { return BadRequest(ModelState); } 
        
        if (!_authService.HasAccessToResource(Convert.ToInt32(dto.UserId), null, HttpContext.User)) 
            { return Forbid(); }

        try 
        {
            var meal = _mealService.RegisterMeal(dto);
            return CreatedAtAction(nameof(GetMealById), new { userId = dto.UserId, mealId = meal.Id }, meal);
        }     
        catch (Exception ex)
        {
            return BadRequest($"Error registering meal. {ex.Message}");
        }
    }

    [Authorize(Roles = Roles.Admin)]
    [HttpGet("/meals")]
    public ActionResult<IEnumerable<Meal>> GetMeals()
    {
        try
        {
            var meals = _mealService.GetMeals();
            return Ok(meals);
        }
        catch (Exception ex)
        {
            return BadRequest($"Error retrieving all meals. {ex.Message}");
        }
    }
    
    [Authorize(Roles = Roles.Admin + "," + Roles.User)]
    [HttpGet]
    public ActionResult<IEnumerable<Meal>> GetMealsByUser(int userId)
    {
        var requestedUser = _userService.GetUserById(userId);

        if (requestedUser == null)
            return NotFound();

        if (!_authService.HasAccessToResource(Convert.ToInt32(userId), null, HttpContext.User, requestedUser.Role)) 
            { return Forbid(); }

        try 
        {
            var meals = _mealService.GetMealsByUser(userId);
            return Ok(meals);
        }     
        catch (Exception ex)
        {
            return BadRequest($"Error retrieving all meals for user with ID {userId}. {ex.Message}");

        }
    }

    [Authorize(Roles = Roles.Admin + "," + Roles.User)]
    [HttpGet("{mealId}")]
    public IActionResult GetMealById(int mealId, int userId)
    {
        var requestedUser = _userService.GetUserById(userId);

        if (requestedUser == null)
            return NotFound();

        if (!_authService.HasAccessToResource(Convert.ToInt32(userId), null, HttpContext.User, requestedUser.Role)) 
            { return Forbid(); }

        try
        {
            var meal = _mealService.GetMealById(mealId, userId);
            return Ok(meal);
        }
        catch (KeyNotFoundException knfex)
        {
            return NotFound($"Meal with ID {mealId} was not found. {knfex.Message}");
        }
        catch (Exception ex)
        {
            return BadRequest($"Error retrieving meal with ID {mealId}. {ex.Message}");
        }
    }

    [Authorize(Roles = Roles.Admin + "," + Roles.User)]
    [HttpPut("{mealId}")]
    public IActionResult UpdateMeal(int mealId, MealCreateUpdateDto dto)
    {
        if (!ModelState.IsValid)  { return BadRequest(ModelState); } 

        if (!_authService.HasAccessToResource(Convert.ToInt32(dto.UserId), null, HttpContext.User)) 
            { return Forbid(); }
            
        try 
        {
            _mealService.UpdateMeal(mealId, dto);
            return Ok("Meal updated successfully.");
        }     
        catch (KeyNotFoundException knfex)
        {
            return NotFound($"Meal with ID {mealId} was not found. {knfex.Message}");
        }
        catch (Exception ex)
        {
            return BadRequest($"Error updating meal with ID {mealId}. {ex.Message}");
        }
    }

    [Authorize(Roles = Roles.Admin + "," + Roles.User)]
    [HttpDelete("{mealId}")]
    public IActionResult DeleteMeal(int mealId, int userId)
    {
        if (!_authService.HasAccessToResource(Convert.ToInt32(userId), null, HttpContext.User)) 
            { return Forbid(); }

        try
        {
            _mealService.DeleteMeal(mealId, userId);
            return Ok("Meal deleted successfully.");
        }
        catch (KeyNotFoundException knfex)
        {
            return NotFound($"Meal with ID {mealId} was not found. {knfex.Message}");
        }
        catch (Exception ex)
        {
            return BadRequest($"Error deleting meal with ID {mealId}. {ex.Message}");
        }
    }
}
