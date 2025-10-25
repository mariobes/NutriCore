using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using NutriCore.Data;
using NutriCore.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using static NutriCore.Models.User;

namespace NutriCore.Business;

public class AuthService : IAuthService
{
    private readonly IUserRepository _repository;
    private readonly IConfiguration _configuration;

    public AuthService(IUserRepository repository, IConfiguration configuration)
    {
        _repository = repository;
        _configuration = configuration;
    }

    public User CheckLogin(string email, string password)
    {
        var normalizedEmail = email.Trim().ToLower();
        var user = _repository.GetUserByEmail(normalizedEmail);

        if (user == null || !PasswordHasher.Verify(password, user.Password))
        {
            return null;
        }

        return user;
    }

    public string GenerateJwtToken(User user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role)
        };

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddDays(30),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public bool HasAccessToResource(int? requestedUserID, string requestedUserEmail, ClaimsPrincipal user) 
    {
        if (requestedUserID.HasValue)
        {
            var userIdClaim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim is null || !int.TryParse(userIdClaim.Value, out int userId)) 
            { 
                return false; 
            }
            var isOwnResource = userId == requestedUserID;

            var roleClaim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
            var isAdmin = roleClaim != null && roleClaim.Value == Roles.Admin;
            
            var hasAccess = isOwnResource || isAdmin;
            return hasAccess;
        }
        else if (!string.IsNullOrEmpty(requestedUserEmail))
        {
            var emailClaim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);

            var isOwnResource = emailClaim?.Value.Equals(requestedUserEmail, StringComparison.OrdinalIgnoreCase) ?? false;

            var roleClaim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
            var isAdmin = roleClaim != null && roleClaim.Value == Roles.Admin;

            return isOwnResource || isAdmin;
        }
        return false;
    }
}