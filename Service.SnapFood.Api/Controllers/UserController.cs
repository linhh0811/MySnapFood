using Microsoft.AspNetCore.Mvc;
using Service.SnapFood.Application.Dtos;
using Service.SnapFood.Application.Interfaces;
using Service.SnapFood.Application.Service;

using Service.SnapFood.Share.Query;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Service.SnapFood.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
      

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

    

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto item)
        {

            var tokenString = await _userService.LoginAsync(item);
            if (tokenString == null)
            {
                return Unauthorized(new { Message = "Thông tin đăng nhập không chính xác" });
            }

            return Ok(tokenString);
        }



        [HttpGet("current")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId)) return Unauthorized();
            var user = await _userService.GetByIdAsync(Guid.Parse(userId));
            return Ok(new UserDto { Id = user.Id, FullName = user.FullName, Email = user.Email });
        }




        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto item)
        {

            var result = await _userService.RegisterAsync(item);
            return Ok();

        }
        [HttpPost("GetPaged")]
        public IActionResult GetPage(BaseQuery query)
        {
            var result = _userService.GetPaged(query);
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            try
            {
                var user = await _userService.GetByIdAsync(id);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}