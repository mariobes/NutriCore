using System.Security.Claims;
using NutriCore.Models;

namespace NutriCore.Business;

public interface IAuthService
{
    User CheckLogin(string email, string password);
    string GenerateJwtToken(User user);
    bool HasAccessToResource(int? requestedUserID, string requestedUserEmail, ClaimsPrincipal user, string? requestedUserRole = null) ;
}