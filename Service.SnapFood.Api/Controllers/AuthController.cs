using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Service.SnapFood.Application.Dtos;
using Service.SnapFood.Application.Interfaces;

namespace Service.SnapFood.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IStaffService _staffService;
        public AuthController(IStaffService staffService)
        {
            _staffService = staffService;
        }
        [HttpPost("Login")]
        public IActionResult Login(LoginDto login)
        {
             var tokenString =  _staffService.Login(login);
            if (tokenString == null)
            {
                return Unauthorized(new { Message = "Thông tin đăng nhập không chính xác" });
            }
           
            return Ok(tokenString);




        }
    }
}
