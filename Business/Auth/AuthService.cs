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
    private readonly IGenericRepository<User> _repository;
    private readonly IConfiguration _configuration;

    public AuthService(IGenericRepository<User> repository, IConfiguration configuration)
    {
        _repository = repository;
        _configuration = configuration;
    }

    public User CheckLogin(string email, string password)
    {
        var normalizedEmail = email.Trim().ToLower();
        var user = _repository.GetEntityByEmail(normalizedEmail);

        if (user == null || !PasswordHasher.Verify(password, user.Password))
        {
            return null;
        }

        return user;
    }

    public string GenerateJwtToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var secretKey = _configuration["JWT:SecretKey"];
        var key = Encoding.UTF8.GetBytes(secretKey ?? throw new InvalidOperationException("JWT SecretKey is not configured.")); 
        
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Role, user.Role)
        };

        if (!string.IsNullOrWhiteSpace(user.Email))
            claims.Add(new Claim(ClaimTypes.Email, user.Email));

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(30),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
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

            var isOwnResource = emailClaim != null && emailClaim.Value.Equals(requestedUserEmail.Trim(), StringComparison.OrdinalIgnoreCase);

            var roleClaim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
            var isAdmin = roleClaim != null && roleClaim.Value == Roles.Admin;

            return isOwnResource || isAdmin;
        }
        
        return false;
    }
}
