using Microsoft.AspNetCore.Mvc;
using NutriCore.Business;
using NutriCore.Models;
using Microsoft.AspNetCore.Authorization;

namespace NutriCore.API.Controllers;

[ApiController]
[Route("[controller]")]
public class FoodsController : ControllerBase
{
    private readonly IFoodService _foodService;
    private readonly IAuthService _authService;

    public FoodsController(IFoodService foodService, IAuthService authService)
    {
        _foodService = foodService;
        _authService = authService;
    }

    [Authorize(Roles = Roles.Admin + "," + Roles.User)]
    [HttpPost]
    public IActionResult CreateFood([FromBody] FoodCreateUpdateDto dto)
    {
        if (!ModelState.IsValid) { return BadRequest(ModelState); } 
        
        if (!_authService.HasAccessToResource(Convert.ToInt32(dto.UserId), null, HttpContext.User)) 
            { return Forbid(); }

        try 
        {
            var food = _foodService.RegisterFood(dto);
            return CreatedAtAction(nameof(GetFoodById), new { foodId = food.Id }, food);
        }     
        catch (Exception ex)
        {
            return BadRequest($"Error registering food. {ex.Message}");
        }
    }

    [Authorize(Roles = Roles.Admin)]
    [HttpGet]
    public ActionResult<IEnumerable<Food>> GetAllFoods()
    {
        try
        {
            var foods = _foodService.GetAllFoods();
            return Ok(foods);
        }
        catch (Exception ex)
        {
            return BadRequest($"Error retrieving all foods. {ex.Message}");
        }
    }
    
    [Authorize(Roles = Roles.Admin + "," + Roles.User)]
    [HttpGet("user/{userId}")]
    public ActionResult<IEnumerable<Food>> GetAllFoodsByUser(int userId)
    {
        if (!_authService.HasAccessToResource(Convert.ToInt32(userId), null, HttpContext.User)) 
            { return Forbid(); }

        try 
        {
            var foods = _foodService.GetAllFoodsByUser(userId);
            return Ok(foods);
        }     
        catch (Exception ex)
        {
            return BadRequest($"Error retrieving all foods for user with ID: {userId}. {ex.Message}");

        }
    }

    [Authorize(Roles = Roles.Admin + "," + Roles.User)]
    [HttpGet("{foodId}")]
    public IActionResult GetFoodById(int foodId, int userId)
    {
        if (!_authService.HasAccessToResource(Convert.ToInt32(userId), null, HttpContext.User)) 
            { return Forbid(); }

        try
        {
            var food = _foodService.GetFoodById(foodId, userId);
            return Ok(food);
        }
        catch (KeyNotFoundException knfex)
        {
            return NotFound($"Food with ID: {foodId} was not found. {knfex.Message}");
        }
        catch (Exception ex)
        {
            return BadRequest($"Error retrieving food with ID: {foodId}. {ex.Message}");
        }
    }

    [Authorize(Roles = Roles.Admin + "," + Roles.User)]
    [HttpPut("{foodId}")]
    public IActionResult UpdateFood(int foodId, FoodCreateUpdateDto dto)
    {
        if (!ModelState.IsValid)  { return BadRequest(ModelState); } 

        if (!_authService.HasAccessToResource(Convert.ToInt32(dto.UserId), null, HttpContext.User)) 
            { return Forbid(); }
            
        try 
        {
            _foodService.UpdateFood(foodId, dto);
            return Ok("Food updated successfully.");
        }     
        catch (KeyNotFoundException knfex)
        {
            return NotFound($"Food with ID: {foodId} was not found. {knfex.Message}");
        }
        catch (Exception ex)
        {
            return BadRequest($"Error updating food with ID: {foodId}. {ex.Message}");
        }
    }

    [Authorize(Roles = Roles.Admin + "," + Roles.User)]
    [HttpDelete("{foodId}")]
    public IActionResult DeleteFood(int foodId, int userId)
    {
        if (!_authService.HasAccessToResource(Convert.ToInt32(userId), null, HttpContext.User)) 
            { return Forbid(); }

        try
        {
            _foodService.DeleteFood(foodId, userId);
            return Ok("Food deleted successfully.");
        }
        catch (KeyNotFoundException knfex)
        {
            return NotFound($"Food with ID: {foodId} was not found. {knfex.Message}");
        }
        catch (Exception ex)
        {
            return BadRequest($"Error deleting food with ID: {foodId}. {ex.Message}");
        }
    }
}
