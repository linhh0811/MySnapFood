using Microsoft.AspNetCore.Mvc;
using Service.SnapFood.Application.Dtos;
using Service.SnapFood.Application.Interfaces;
using Service.SnapFood.Share.Query;
using System;
using System.Threading.Tasks;

namespace Service.SnapFood.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private readonly IUserService _userService;

        public StaffController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("GetPaged")]
        public IActionResult GetPage(BaseQuery query)
        {
            var result = _userService.GetPaged(query);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var users = await _userService.GetAllAsync();
            return Ok(users);
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

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] UserDto item)
        {
            var result = await _userService.CreateAsync(item);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = result }, item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] UserDto item)
        {
            var result = await _userService.UpdateAsync(id, item);
            if (!result)
            {
                return BadRequest("Cập nhật không thành công");
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var result = await _userService.DeleteAsync(id);
            if (!result)
            {
                return BadRequest("Xóa không thành công");
            }
            return NoContent();
        }

        [HttpPut("{id}/Approve")]
        public async Task<IActionResult> Approve(Guid id)
        {
            var result = await _userService.ApproveAsync(id);
            if (!result)
            {
                return BadRequest("Duyệt không thành công");
            }
            return NoContent();
        }

        [HttpPut("{id}/Reject")]
        public async Task<IActionResult> RejectAsync(Guid id)
        {
            var result = await _userService.RejectAsync(id);
            if (!result)
            {
                return BadRequest("Hủy duyệt không thành công");
            }
            return NoContent();
        }
    }
}