using Microsoft.AspNetCore.Mvc;
using Service.SnapFood.Application.Dtos;
using Service.SnapFood.Application.Interfaces;
using System;
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
            var user = await _userService.LoginAsync(item);
            if (user == null)
            {
                return Unauthorized("Email hoặc mật khẩu không đúng");
            }
            return Ok(user);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] UserDto item)
        {
            var result = await _userService.CreateAsync(item);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = result }, item);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound("Không tìm thấy người dùng");
            }
            return Ok(user);
        }
    }
}