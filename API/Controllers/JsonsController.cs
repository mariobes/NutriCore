using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NutriCore.Models;

namespace NutriCore.API.Controllers;

[ApiController]
[Route("[controller]")]
public class JsonsController : ControllerBase
{
    private readonly IWebHostEnvironment _env;

    public JsonsController(IWebHostEnvironment env)
    {
        _env = env;
    }

    [Authorize(Roles = Roles.Admin)]
    [HttpGet("{fileName}")]
    public IActionResult GetJson(string fileName)
    {
        try 
        {
            var path = Path.Combine(_env.ContentRootPath, "Json", "Data", $"{fileName.ToLower()}.json");

            if (!System.IO.File.Exists(path))
            {
                return NotFound($"File {fileName.ToLower()}.json not found.");  
            }

            var jsonContent = System.IO.File.ReadAllText(path);
            return Content(jsonContent, "application/json");
        }     
        catch (Exception ex)
        {
            return BadRequest($"Error reading the file {fileName.ToLower()}.json. {ex.Message}");
        }
    }

    [Authorize(Roles = Roles.Admin)]
    [HttpPost("{fileName}")]
    public IActionResult SaveJson(string fileName, [FromBody] string jsonData)
    {
        var path = Path.Combine(_env.ContentRootPath, "Json", "Data", $"{fileName}.json");

        var jsonString = System.Text.Json.JsonSerializer.Serialize(jsonData, new System.Text.Json.JsonSerializerOptions
        {
            WriteIndented = true
        });

        System.IO.File.WriteAllText(path, jsonString);

        return Ok(new { Message = "File saved successfully.", File = fileName });
    }
}