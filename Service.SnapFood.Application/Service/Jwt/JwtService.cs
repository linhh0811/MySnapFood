﻿using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Service.SnapFood.Application.Interfaces.Jwt;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Service.SnapFood.Application.Service.Jwt
{
    public class JwtService : IJwtService
    {
        private readonly string _secretKey;
        private readonly int _expiresInMinutes;

        public JwtService(IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("JwtSettings");
            _secretKey = jwtSettings["SecretKey"];
            _expiresInMinutes = int.Parse(jwtSettings["ExpiresInMinutes"]);
        }

        // Tạo JWT Token
        public string GenerateToken(string username, string role)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.Role, role),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            var token = new JwtSecurityToken(
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
