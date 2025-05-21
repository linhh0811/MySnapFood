using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.SnapFood.Application.Dtos;
using Service.SnapFood.Application.Interfaces;
using Service.SnapFood.Application.Service;
using Service.SnapFood.Share.Query;

namespace Service.SnapFood.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComboController : ControllerBase
    {
        private readonly IComboService _comboService;

        public ComboController(IComboService comboService)
        {
            _comboService = comboService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var combo = await _comboService.GetAllAsync();
            return Ok(combo);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid Id)
        {
            var combo = await _comboService.GetByIdAsync(Id);
            if (combo == null)
            {
                return NotFound("Không tìm thấy combo");
            }
            return Ok(combo);
        }

        [HttpPost("GetPaged")]
        public IActionResult GetPage(BaseQuery query)
        {
            var result = _comboService.GetPaged(query);
            return Ok(result);
        }

        [HttpPost()]
        public async Task<IActionResult> Create([FromBody] ComboDto item)
        {
            var result = await _comboService.CreateAsync(item);
            return CreatedAtAction(nameof(GetById), new { id = result }, item);
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> Update(Guid Id, ComboDto item)
        {
            var result = await _comboService.UpdateAsync(Id, item);
            if (!result)
            {
                return BadRequest("Cập nhật không thành công");
            }
            return NoContent();
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var result = await _comboService.DeleteAsync(Id);
            if (!result)
            {
                return BadRequest("Xoá không thành công");
            }
            return NoContent();
        }

        [HttpPut("{Id}/Approve")]
        public async Task<IActionResult> Approve(Guid Id)
        {
            var result = await _comboService.ApproveAsync(Id);
            if (!result)
            {
                return BadRequest("Duyệt không thành công");
            }
            return NoContent();
        }
        [HttpPut("{Id}/Reject")]
        public async Task<IActionResult> Reject(Guid Id)
        {
            var result = await _comboService.RejectAsync(Id);
            if (!result)
            {
                return BadRequest("Huỷ duyệt không thành công");
            }
            return NoContent();
        }

    }
}
