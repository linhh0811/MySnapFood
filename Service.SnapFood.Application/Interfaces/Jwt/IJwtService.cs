using Service.SnapFood.Application.Dtos;
using System.Security.Claims;

namespace Service.SnapFood.Application.Interfaces.Jwt
{
    public interface IJwtService
    {
        string GenerateToken(AuthDto authDto);
        ClaimsPrincipal ValidateToken(string token);
    }
}
