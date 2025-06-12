using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Service.SnapFood.Share.Interface.Extentions;
using Service.SnapFood.Share.Model.Commons;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Service.SnapFood.Infrastructure.Middleware
{
    public class JwtMiddleware
    {
        private readonly IConfiguration _configuration;
        private readonly RequestDelegate _next;
        public JwtMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        public async Task InvokeAsync(HttpContext context, IRequestContext requestContext)
        {
            string? token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token is not null)
            {
                var identity = GetClaimsIdentity(token);
                var user = new ClaimsPrincipal(identity);
                string userId = user.FindFirst("user_id")?.Value ?? string.Empty;
                string userName = user.Identity?.Name ?? string.Empty;
                if (!string.IsNullOrEmpty(userId))
                {
                    CurrentUser CurrentUser = new CurrentUser()
                    {
                        UserId = Guid.Parse(userId),
                        UserName = userName,
                    };
                    requestContext.CurrentUser = CurrentUser;
                }               
                
            }
            await _next(context);
        }
        private ClaimsIdentity GetClaimsIdentity(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            var claims = jwtToken.Claims;
            return new ClaimsIdentity(claims, "jwt");
        }
    } 
}
