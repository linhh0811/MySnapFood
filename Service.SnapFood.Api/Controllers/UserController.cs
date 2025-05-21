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
            try
            {
                var user = await _userService.LoginAsync(item);
                if (user == null)
                    return Unauthorized("Email hoặc mật khẩu không đúng");
                return Ok(user);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto item)
        {
            try
            {
                var result = await _userService.RegisterAsync(item);
                return CreatedAtAction(nameof(GetByIdAsync), new { id = result }, item);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
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