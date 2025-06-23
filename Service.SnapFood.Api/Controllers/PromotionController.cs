using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.SnapFood.Application.Dtos;
using Service.SnapFood.Application.Interfaces;
using Service.SnapFood.Share.Query;

namespace Service.SnapFood.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromotionController : ControllerBase
    {
        private readonly IPromotionService _promotionService;
        public PromotionController(IPromotionService promotionService)
        {
            _promotionService = promotionService;
        }

        [HttpGet]
        public IActionResult GetPromotionActivate()
        {
            var combo =  _promotionService.GetPromotionActivate();
            return Ok(combo);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid Id)
        {
            var combo = await _promotionService.GetByIdAsync(Id);
            if (combo == null)
            {
                return NotFound("Không tìm thấy promotion");
            }
            return Ok(combo);
        }

        [HttpPost("GetPaged")]
        public IActionResult GetPage(BaseQuery query)
        {
            var result = _promotionService.GetPaged(query);
            return Ok(result);
        }

        [HttpPost()]
        public async Task<IActionResult> Create([FromBody] PromotionDto item)
        {
            var result = await _promotionService.CreateAsync(item);
            return CreatedAtAction(nameof(GetById), new { id = result }, item);
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> Update(Guid Id, PromotionDto item)
        {
            var result = await _promotionService.UpdateAsync(Id, item);
            if (!result)
            {
                return BadRequest("Cập nhật không thành công");
            }
            return NoContent();
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var result = await _promotionService.DeleteAsync(Id);
            if (!result)
            {
                return BadRequest("Xoá không thành công");
            }
            return NoContent();
        }
       
        [HttpPut("{Id}/Approve")]
        public async Task<IActionResult> Approve(Guid Id)
        {
            var result = await _promotionService.ApproveAsync(Id);
            if (!result)
            {
                return BadRequest("Duyệt không thành công");
            }
            return NoContent();
        }
        [HttpPut("{Id}/Reject")]
        public async Task<IActionResult> Reject(Guid Id)
        {
            var result = await _promotionService.RejectAsync(Id);
            if (!result)
            {
                return BadRequest("Huỷ duyệt không thành công");
            }
            return NoContent();
        }
    }
}
