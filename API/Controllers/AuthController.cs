using Microsoft.AspNetCore.Mvc;
using NutriCore.Business;
using NutriCore.Models;

namespace NutriCore.API.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IAuthService _authService;

    public AuthController(IUserService userService, IAuthService authService)
    {
        _userService = userService;
        _authService = authService;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] UserLoginDto dto)
    {
        try
        {
            var user = _authService.CheckLogin(dto.Email, dto.Password);
            if (user != null)
            {
                var token = _authService.GenerateJwtToken(user);
                return Ok(token);
            }
            else
            {
                return NotFound("No user matches the provided credentials");
            }
        }
        catch (KeyNotFoundException knfex)
        {
            return NotFound($"No user was found. {knfex.Message}");
        }
        catch (Exception ex)
        {
            return BadRequest($"No user was found. {ex.Message}");
        }
    }

    [HttpPost("register")]
    public IActionResult Register([FromBody] UserCreateUpdateDto dto)
    {
        if (!ModelState.IsValid)  { return BadRequest(ModelState); } 

        try 
        {
            var user = _userService.RegisterUser(dto);
            return CreatedAtAction(nameof(Login), new { userId = user.Id }, user);
        }     
        catch (Exception ex)
        {
            return BadRequest($"Error registering the user. {ex.Message}");
        }
    }
}
