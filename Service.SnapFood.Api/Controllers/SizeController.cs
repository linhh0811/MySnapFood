using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.SnapFood.Application.Dtos;
using Service.SnapFood.Application.Interfaces;
using Service.SnapFood.Application.Service;

namespace Service.SnapFood.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SizeController : ControllerBase
    {
        private readonly ISizeService _sizeService;
        public SizeController(ISizeService sizeService)
        {
            _sizeService = sizeService;
        }
        [HttpGet()]
        public async Task<IActionResult> GetAll()
        {
            var sizes = await _sizeService.GetAllAsync();
            return Ok(sizes);
        }
        [HttpGet("GetSizeSelect")]
        public IActionResult GetSizeSelect()
        {
            var sizes =  _sizeService.GetSizeSelect();
            return Ok(sizes);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var size = await _sizeService.GetByIdAsync(id);
            if (size is null) return NotFound();
            return Ok(size);
        }
        [HttpGet("tree")]
        public async Task<IActionResult> GetSizeTree()
        {
            var sizeTree = await _sizeService.GetSizeTreeAsync();
            return Ok(sizeTree);
        }
        [HttpPost()]
        public async Task<IActionResult> Create([FromBody] SizeDto item)
        {
            if (item is null) return BadRequest();
            var id = await _sizeService.CreateAsync(item);
            return CreatedAtAction(nameof(GetById), new { id }, item);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id,SizeDto item)
        {
            if (item is null) return BadRequest();
            var result = await _sizeService.UpdateAsync(id, item);
            if (!result) return BadRequest("Cập nhật không thành công");
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _sizeService.DeleteAsync(id);
            if (!result)  return BadRequest("Xóa không thành công");
            return NoContent();
        }
        [HttpPut("{Id}/Approve")]
        public async Task<IActionResult> Approve(Guid Id)
        {
            var result = await _sizeService.ApproveAsync(Id);
            if (!result)
            {
                return BadRequest("Duyệt không thành công");
            }
            return NoContent();
        }
        [HttpPut("{Id}/CheckReject")]
        public IActionResult CheckReject(Guid Id)
        {
            var result = _sizeService.CheckReject(Id);

            return Ok(result);
        }
        [HttpPut("{Id}/Reject")]
        public async Task<IActionResult> Reject(Guid Id)
        {
            var result = await _sizeService.RejectAsync(Id);
            if (!result)
            {
                return BadRequest("Huỷ duyệt không thành công");
            }
            return NoContent();
        }
    }
}
