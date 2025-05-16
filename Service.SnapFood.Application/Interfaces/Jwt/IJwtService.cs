using System.Security.Claims;

namespace Service.SnapFood.Application.Interfaces.Jwt
{
    public interface IJwtService
    {
        string GenerateToken(string username, string role);
        ClaimsPrincipal ValidateToken(string token);
    }
}
