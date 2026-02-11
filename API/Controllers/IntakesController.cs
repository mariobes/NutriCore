using Microsoft.AspNetCore.Mvc;
using NutriCore.Business;
using NutriCore.Models;
using Microsoft.AspNetCore.Authorization;

namespace NutriCore.API.Controllers;

[ApiController]
[Route("Users/{userId}/intakes")]
public class IntakesController : ControllerBase
{
    private readonly IIntakeService _intakeService;
    private readonly IAuthService _authService;
    private readonly IUserService _userService;

    public IntakesController(IIntakeService intakeService, IAuthService authService, IUserService userService)
    {
        _intakeService = intakeService;
        _authService = authService;
        _userService = userService;
    }

    [Authorize(Roles = Roles.Admin + "," + Roles.User)]
    [HttpPost]
    public IActionResult CreateIntake([FromBody] IntakeCreateUpdateDto dto)
    {
        if (!ModelState.IsValid) { return BadRequest(ModelState); } 
        
        if (!_authService.HasAccessToResource(Convert.ToInt32(dto.UserId), null, HttpContext.User)) 
            { return Forbid(); }

        try 
        {
            var intake = _intakeService.RegisterIntake(dto);
            return CreatedAtAction(nameof(GetIntakeById), new { userId = dto.UserId, intakeId = intake.Id }, intake);
        }     
        catch (Exception ex)
        {
            return BadRequest($"Error registering intake. {ex.Message}");
        }
    }

    [Authorize(Roles = Roles.Admin)]
    [HttpGet("/intakes")]
    public ActionResult<IEnumerable<Intake>> GetIntakes()
    {
        try
        {
            var intakes = _intakeService.GetIntakes();
            return Ok(intakes);
        }
        catch (Exception ex)
        {
            return BadRequest($"Error retrieving all intakes. {ex.Message}");
        }
    }
    
    [Authorize(Roles = Roles.Admin + "," + Roles.User)]
    [HttpGet]
    public ActionResult<IEnumerable<Intake>> GetIntakesByUser(int userId)
    {
        var requestedUser = _userService.GetUserById(userId);

        if (requestedUser == null)
            return NotFound();

        if (!_authService.HasAccessToResource(Convert.ToInt32(userId), null, HttpContext.User, requestedUser.Role)) 
            { return Forbid(); }

        try 
        {
            var intakes = _intakeService.GetIntakesByUser(userId);
            return Ok(intakes);
        }     
        catch (Exception ex)
        {
            return BadRequest($"Error retrieving all intakes for user with ID {userId}. {ex.Message}");
        }
    }

    [Authorize(Roles = Roles.Admin + "," + Roles.User)]
    [HttpGet("{intakeId}")]
    public IActionResult GetIntakeById(int intakeId, int userId)
    {
        var requestedUser = _userService.GetUserById(userId);

        if (requestedUser == null)
            return NotFound();

        if (!_authService.HasAccessToResource(Convert.ToInt32(userId), null, HttpContext.User, requestedUser.Role)) 
            { return Forbid(); }

        try
        {
            var intake = _intakeService.GetIntakeById(intakeId);
            return Ok(intake);
        }
        catch (KeyNotFoundException knfex)
        {
            return NotFound($"Intake with ID: {intakeId} was not found. {knfex.Message}");
        }
        catch (Exception ex)
        {
            return BadRequest($"Error retrieving intake with ID {intakeId}. {ex.Message}");
        }
    }

    [Authorize(Roles = Roles.Admin + "," + Roles.User)]
    [HttpPut("{intakeId}")]
    public IActionResult UpdateIntake(int intakeId, IntakeCreateUpdateDto dto)
    {
        if (!ModelState.IsValid)  { return BadRequest(ModelState); } 

        if (!_authService.HasAccessToResource(Convert.ToInt32(dto.UserId), null, HttpContext.User)) 
            { return Forbid(); }
            
        try 
        {
            _intakeService.UpdateIntake(intakeId, dto);
            return Ok("Intake updated successfully.");
        }     
        catch (KeyNotFoundException knfex)
        {
            return NotFound($"Intake with ID {intakeId} was not found. {knfex.Message}");
        }
        catch (Exception ex)
        {
            return BadRequest($"Error updating intake with ID {intakeId}. {ex.Message}");
        }
    }

    [Authorize(Roles = Roles.Admin + "," + Roles.User)]
    [HttpDelete("{intakeId}")]
    public IActionResult DeleteIntake(int intakeId, int userId)
    {
        if (!_authService.HasAccessToResource(Convert.ToInt32(userId), null, HttpContext.User)) 
            { return Forbid(); }

        try
        {
            _intakeService.DeleteIntake(intakeId);
            return Ok("Intake deleted successfully.");
        }
        catch (KeyNotFoundException knfex)
        {
            return NotFound($"Intake with ID {intakeId} was not found. {knfex.Message}");
        }
        catch (Exception ex)
        {
            return BadRequest($"Error deleting intake with ID {intakeId}. {ex.Message}");
        }
    }
}