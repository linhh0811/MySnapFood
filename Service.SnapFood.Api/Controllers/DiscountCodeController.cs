

using Microsoft.AspNetCore.Mvc;
using Service.SnapFood.Application.Dtos;
using Service.SnapFood.Application.Interfaces;
using Service.SnapFood.Domain.Query;

namespace Service.SnapFood.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountCodeController : ControllerBase
    {
        private readonly IDiscountCodeService _discountCodeService;

        public DiscountCodeController(IDiscountCodeService discountCodeService)
        {
            _discountCodeService = discountCodeService;
        }

        [HttpPost("GetPaged")]
        public IActionResult GetPaged([FromBody] VoucherQuery query)
        {
            var result = _discountCodeService.GetPaged(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var discount = await _discountCodeService.GetByIdAsync(id);
            if (discount == null)
            {
                return NotFound("Không tìm thấy mã giảm giá");
            }
            return Ok(discount);
        }

        [HttpGet("DiscountHoatDong/{id}")]
        public async Task<IActionResult> GetDiscountHoatDongById(Guid id)
        {
            var discount = await _discountCodeService.GetDiscountHoatDongById(id);
            if (discount == null)
            {
                return NotFound("Không tìm thấy mã giảm giá");
            }
            return Ok(discount);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DiscountCodeDto item)
        {
            var result = await _discountCodeService.CreateAsync(item);
            return CreatedAtAction(nameof(GetById), new { id = result }, item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] DiscountCodeDto item)
        {
            var result = await _discountCodeService.UpdateAsync(id, item);
            if (!result)
            {
                return BadRequest("Cập nhật không thành công");
            }

            var updatedItem = await _discountCodeService.GetByIdAsync(id);
            return Ok(updatedItem);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _discountCodeService.DeleteAsync(id);
            if (!result)
            {
                return BadRequest("Xóa không thành công");
            }
            return NoContent();
        }

        [HttpPut("{id}/Approve")]
        public async Task<IActionResult> Approve(Guid id)
        {
            var result = await _discountCodeService.ApproveAsync(id);
            if (!result)
            {
                return BadRequest("Duyệt không thành công");
            }
            return NoContent();
        }

        [HttpPut("{id}/Reject")]
        public async Task<IActionResult> Reject(Guid id)
        {
            var result = await _discountCodeService.RejectAsync(id);
            if (!result)
            {
                return BadRequest("Hủy duyệt không thành công");
            }
            return NoContent();
        }
    }
}
