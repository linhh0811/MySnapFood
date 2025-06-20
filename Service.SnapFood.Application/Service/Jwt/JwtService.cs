using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Service.SnapFood.Application.Dtos;
using Service.SnapFood.Application.Interfaces.Jwt;
using Service.SnapFood.Domain.Entitys;
using Service.SnapFood.Domain.Enums;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Service.SnapFood.Application.Service.Jwt
{
    public class JwtService : IJwtService
    {
        private readonly string _secretKey;
        private readonly string _issuer;
        private readonly string _audience;
        private readonly int _expiresInMinutes;

        public JwtService(IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("JwtSettings");
            _secretKey = jwtSettings["SecretKey"] ?? throw new ArgumentNullException("JwtSettings:SecretKey is missing");
            _issuer = jwtSettings["Issuer"] ?? throw new ArgumentNullException("JwtSettings:Issuer is missing");
            _audience = jwtSettings["Audience"] ?? throw new ArgumentNullException("JwtSettings:Audience is missing");

            if (!int.TryParse(jwtSettings["ExpiresInMinutes"], out _expiresInMinutes) || _expiresInMinutes <= 0)
            {
                throw new ArgumentException("JwtSettings:ExpiresInMinutes must be a valid positive integer");
            }
        }

        // Tạo JWT Token
        public string GenerateToken(AuthDto authDto)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, authDto.FullName),
                new Claim("user_id", authDto.Id.ToString()),
                new Claim(ClaimTypes.Email, authDto.Email),


                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            foreach (var role in authDto.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.EnumRole.ToString()));
            }

            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_expiresInMinutes),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        // Xác thực JWT Token
        public ClaimsPrincipal ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_secretKey);

            var parameters = new TokenValidationParameters
            {
                ValidateIssuer = false,  // Không cần kiểm tra Issuer
                ValidateAudience = false,  // Không cần kiểm tra Audience
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };

            return tokenHandler.ValidateToken(token, parameters, out _);
        }
    }
}
