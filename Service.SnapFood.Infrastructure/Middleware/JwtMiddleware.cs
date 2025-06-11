using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Service.SnapFood.Share.Interface.Extentions;
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
            string? token = context.Request.Headers["Authorization"].FirstOrDefault();
            //if (token is not null)
            //{
            //    var identity = GetClaimsIdentity(token);
            //    var user = new ClaimsPrincipal(identity);
            //}
            //await _next(context);
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
