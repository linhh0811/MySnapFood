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
            return Ok(new UserDto { Id = user.Id, FullName = user.FullName, Email = user.Email, Numberphone = user.Numberphone });
        }




        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto item)
        {

            var result = await _userService.RegisterAsync(item);
            return Ok();

        }

        [HttpPut("SendOtp")]
        public async Task<IActionResult> SendOtp([FromBody] OtpConfirmDto item)
        {

            await _userService.SendOtp(item);
            return Ok();

        }
        [HttpPut("VerifyOtp")]
        public async Task<IActionResult> VerifyOtp([FromBody] OtpConfirmDto item)
        {
            try
            {
                 await _userService.VerifyOtp(item);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { status = "ERROR", message = ex.Message });
            }
        }

        [HttpPut("LayMatKhau")]
        public async Task<IActionResult> LayMatKhau([FromBody] OtpConfirmDto item)
        {

            await _userService.LayLaiMatKhau(item);
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

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UserDto model)
        {
            if (id == Guid.Empty || model == null)
                return BadRequest("Dữ liệu không hợp lệ");

            var success = await _userService.UpdateAsync(id, model);
            if (!success)
                return BadRequest("Cập nhật không thành công");

            return Ok(new { message = "Cập nhật thành công" });
        }

        [HttpPut("{Id}/Approve")]
        public async Task<IActionResult> Approve(Guid Id)
        {
            var result = await _userService.ApproveAsync(Id);
            if (!result)
            {
                return BadRequest("Duyệt không thành công");
            }
            return NoContent();
        }

        [HttpPut("{Id}/Reject")]
        public async Task<IActionResult> Reject(Guid Id)
        {
            var result = await _userService.RejectAsync(Id);
            if (!result)
            {
                return BadRequest("Huỷ duyệt không thành công");
            }
            return NoContent();
        }
    }
}