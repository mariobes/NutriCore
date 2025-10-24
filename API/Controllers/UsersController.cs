using Microsoft.AspNetCore.Mvc;
using NutriCore.Business;
using NutriCore.Models;
using Microsoft.AspNetCore.Authorization;

namespace NutriCore.API.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IAuthService _authService;

    public UsersController(IUserService userService, IAuthService authService)
    {
        _userService = userService;
        _authService = authService;
    }

    [Authorize(Roles = Roles.Admin)]
    [HttpGet]
    public ActionResult<IEnumerable<User>> GetAllUsers()
    {
        try 
        {
            var users = _userService.GetAllUsers();
            return Ok(users);
        }     
        catch (Exception ex)
        {
            return BadRequest($"Error retrieving all users. {ex.Message}");

        }
    }

    [Authorize(Roles = Roles.Admin + "," + Roles.User)]
    [HttpGet("{userId}")]
    public IActionResult GetUserById(int userId)
    {
        if (!_authService.HasAccessToResource(Convert.ToInt32(userId), null, HttpContext.User)) 
            { return Forbid(); }

        try
        {
            var user = _userService.GetUserById(userId);
            return Ok(user);
        }
        catch (KeyNotFoundException knfex)
        {
            return NotFound($"User with ID: {userId} was not found. {knfex.Message}");
        }
        catch (Exception ex)
        {
            return BadRequest($"Error retrieving user with ID: {userId}. {ex.Message}");
        }
    }

    [Authorize(Roles = Roles.Admin + "," + Roles.User)]
    [HttpGet("by-email")]
    public IActionResult GetUserByEmail(string email)
    {
        if (!_authService.HasAccessToResource(null, email, HttpContext.User)) 
            { return Forbid(); }

        try
        {
            var user = _userService.GetUserByEmail(email);
            return Ok(user);
        }
        catch (KeyNotFoundException knfex)
        {
            return NotFound($"User with email: {email} was not found. {knfex.Message}");
        }
        catch (Exception ex)
        {
            return BadRequest($"Error retrieving user with email: {email}. {ex.Message}");
        }
    }

    [Authorize(Roles = Roles.Admin + "," + Roles.User)]
    [HttpPut("{userId}")]
    public IActionResult UpdateUser(int userId, UserCreateUpdateDto dto)
    {
        if (!ModelState.IsValid)  { return BadRequest(ModelState); } 

        if (!_authService.HasAccessToResource(Convert.ToInt32(userId), null, HttpContext.User)) 
            { return Forbid(); }

        try 
        {
            _userService.UpdateUser(userId, dto);
            return Ok("User updated successfully.");
        }     
        catch (KeyNotFoundException knfex)
        {
            return NotFound($"User with ID: {userId} was not found. {knfex.Message}");
        }
        catch (Exception ex)
        {
            return BadRequest($"Error updating user with ID: {userId}. {ex.Message}");
        }
    }

    [Authorize(Roles = Roles.Admin + "," + Roles.User)]
    [HttpDelete("{userId}")]
    public IActionResult DeleteUser(int userId)
    {
        if (!_authService.HasAccessToResource(Convert.ToInt32(userId), null, HttpContext.User)) 
            { return Forbid(); }

        try
        {
            _userService.DeleteUser(userId);
            return Ok("User deleted successfully.");
        }
        catch (KeyNotFoundException knfex)
        {
            return NotFound($"User with ID: {userId} was not found. {knfex.Message}");
        }
        catch (Exception ex)
        {
            return BadRequest($"Error deleting user with ID: {userId}. {ex.Message}");
        }
    }
}
