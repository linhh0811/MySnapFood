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
        private readonly IStaffService _staffService;

        public StaffController(IStaffService staffService)
        {
            _staffService = staffService;
        }

        [HttpPost("GetPaged")]
        public IActionResult GetPage([FromBody] BaseQuery query)
        {
            try
            {
                var result = _staffService.GetPaged(query);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var users = await _staffService.GetAllAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            try
            {
                var user = await _staffService.GetByIdAsync(id);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] StaffDto item)
        {
            try
            {
                var result = await _staffService.CreateAsync(item);
                //return CreatedAtAction(nameof(GetByIdAsync), new { id = result }, item);
                return Ok(new { id = result, message = "Thêm nhân viên thành công" });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] StaffDto item)
        {
            try
            {
                var result = await _staffService.UpdateAsync(id, item);
                if (!result)
                    return BadRequest("Cập nhật không thành công");
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            try
            {
                var result = await _staffService.DeleteAsync(id);
                if (!result)
                    return BadRequest("Xóa không thành công");
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}/Approve")]
        public async Task<IActionResult> Approve(Guid id)
        {
            try
            {
                var result = await _staffService.ApproveAsync(id);
                if (!result)
                    return BadRequest("Duyệt không thành công");
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}/Reject")]
        public async Task<IActionResult> RejectAsync(Guid id)
        {
            try
            {
                var result = await _staffService.RejectAsync(id);
                if (!result)
                    return BadRequest("Hủy duyệt không thành công");
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}