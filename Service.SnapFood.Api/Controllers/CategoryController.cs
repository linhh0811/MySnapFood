using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.SnapFood.Application.Dtos;
using Service.SnapFood.Application.Interfaces;
using Service.SnapFood.Share.Query;

namespace Service.SnapFood.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _categoryService.GetAllAsync();
            return Ok(products);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid Id)
        {
            var result = await _categoryService.GetByIdAsync(Id);
            if (result == null)
            {
                return NotFound("Không tìm thấy phân loại");
            }
            return Ok(result);
        }
        [HttpPost("GetPaged")]
        public IActionResult GetPage(BaseQuery query)
        {
            var result = _categoryService.GetPaged(query);
            return Ok(result);
        }

        [HttpPost()]
        public async Task<IActionResult> Create([FromBody] CategoryDto item)
        {
            var result = await _categoryService.CreateAsync(item);
            return CreatedAtAction(nameof(GetById), new { id = result }, item);
        }
        [HttpPut("{Id}")]
        public async Task<IActionResult> Update(Guid Id, CategoryDto item)
        {
            var result = await _categoryService.UpdateAsync(Id, item);
            if (!result)
            {
                return BadRequest("Cập nhật không thành công");
            }
            return NoContent();
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var result = await _categoryService.DeleteAsync(Id);
            if (!result)
            {
                return BadRequest("Xoá không thành công");
            }
            return NoContent();
        }

        [HttpPut("{Id}/Approve")]
        public async Task<IActionResult> Approve(Guid Id)
        {
            var result = await _categoryService.ApproveAsync(Id);
            if (!result)
            {
                return BadRequest("Duyệt không thành công");
            }
            return NoContent();
        }
        [HttpPut("{Id}/Reject")]
        public async Task<IActionResult> Reject(Guid Id)
        {
            var result = await _categoryService.RejectAsync(Id);
            if (!result)
            {
                return BadRequest("Huỷ duyệt không thành công");
            }
            return NoContent();
        }
    }
}
