using Microsoft.AspNetCore.Mvc;
using NutriCore.Business;
using NutriCore.Models;
using Microsoft.AspNetCore.Authorization;

namespace NutriCore.API.Controllers;

[ApiController]
[Route("[controller]")]
public class MealsController : ControllerBase
{
    private readonly IMealService _mealService;
    private readonly IAuthService _authService;

    public MealsController(IMealService mealService, IAuthService authService)
    {
        _mealService = mealService;
        _authService = authService;
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
            return CreatedAtAction(nameof(GetMealById), new { mealId = meal.Id }, meal);
        }     
        catch (Exception ex)
        {
            return BadRequest($"Error registering meal. {ex.Message}");
        }
    }

    [Authorize(Roles = Roles.Admin)]
    [HttpGet]
    public ActionResult<IEnumerable<Meal>> GetAllMeals()
    {
        try
        {
            var meals = _mealService.GetAllMeals();
            return Ok(meals);
        }
        catch (Exception ex)
        {
            return BadRequest($"Error retrieving all meals. {ex.Message}");
        }
    }
    
    [Authorize(Roles = Roles.Admin + "," + Roles.User)]
    [HttpGet("user/{userId}")]
    public ActionResult<IEnumerable<Meal>> GetAllMealsByUser(int userId)
    {
        if (!_authService.HasAccessToResource(Convert.ToInt32(userId), null, HttpContext.User)) 
            { return Forbid(); }

        try 
        {
            var meals = _mealService.GetAllMealsByUser(userId);
            return Ok(meals);
        }     
        catch (Exception ex)
        {
            return BadRequest($"Error retrieving all meals for user with ID: {userId}. {ex.Message}");

        }
    }

    [Authorize(Roles = Roles.Admin + "," + Roles.User)]
    [HttpGet("{mealId}")]
    public IActionResult GetMealById(int mealId, int userId)
    {
        if (!_authService.HasAccessToResource(Convert.ToInt32(userId), null, HttpContext.User)) 
            { return Forbid(); }

        try
        {
            var meal = _mealService.GetMealById(mealId, userId);
            return Ok(meal);
        }
        catch (KeyNotFoundException knfex)
        {
            return NotFound($"Meal with ID: {mealId} was not found. {knfex.Message}");
        }
        catch (Exception ex)
        {
            return BadRequest($"Error retrieving meal with ID: {mealId}. {ex.Message}");
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
            return NotFound($"Meal with ID: {mealId} was not found. {knfex.Message}");
        }
        catch (Exception ex)
        {
            return BadRequest($"Error updating meal with ID: {mealId}. {ex.Message}");
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
            return NotFound($"Meal with ID: {mealId} was not found. {knfex.Message}");
        }
        catch (Exception ex)
        {
            return BadRequest($"Error deleting meal with ID: {mealId}. {ex.Message}");
        }
    }
}
