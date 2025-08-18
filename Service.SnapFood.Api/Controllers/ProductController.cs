using Microsoft.AspNetCore.Mvc;
using Service.SnapFood.Application.Dtos;
using Service.SnapFood.Application.Interfaces;
using Service.SnapFood.Domain.Query;
using Service.SnapFood.Share.Query;

namespace Service.SnapFood.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productService.GetAllAsync();
            return Ok(products);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid Id)
        {
            var result = await _productService.GetByIdAsync(Id);
            if (result == null)
            {
                return NotFound("Không tìm thấy product");
            }
            return Ok(result);
        }
        [HttpPost("GetPaged")]
        public IActionResult GetPage(ProductQuery query)
        {
            var result = _productService.GetPaged(query);
            return Ok(result);
        }

        [HttpPost("GetPageProductCombo")]
        public IActionResult GetPageProductCombo(BaseQuery query)
        {
            var result = _productService.GetProductAndCombo(query);
            return Ok(result);
        }

            [HttpPost()]
        public async Task<IActionResult> Create([FromBody] ProductDto item)
        {
            var result = await _productService.CreateAsync(item);
            return CreatedAtAction(nameof(GetById), new { id = result }, item);
        }
        [HttpPut("{Id}")]
        public async Task<IActionResult> Update(Guid Id, ProductDto item)
        {
            var result = await _productService.UpdateAsync(Id, item);
            if (!result)
            {
                return BadRequest("Cập nhật không thành công");
            }
            return NoContent();
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var result = await _productService.DeleteAsync(Id);
            if (!result)
            {
                return BadRequest("Xoá không thành công");
            }
            return NoContent();
        }
        [HttpGet("{Id}/CheckApprove")]
        public IActionResult CheckApprove(Guid Id)
        {
            var result = _productService.CheckApprove(Id);

            return Ok(result);
        }
        [HttpPut("{Id}/Approve")]
        public async Task<IActionResult> Approve(Guid Id)
        {
            var result = await _productService.ApproveAsync(Id);
            if (!result)
            {
                return BadRequest("Duyệt không thành công");
            }
            return NoContent();
        }
        [HttpPut("{Id}/CheckReject")]
        public IActionResult CheckReject(Guid Id)
        {
            var result = _productService.CheckRejectAsync(Id);

            return Ok(result);
        }
        [HttpPut("{Id}/Reject")]
        public async Task<IActionResult> Reject(Guid Id)
        {
            var result = await _productService.RejectAsync(Id);
            if (!result)
            {
                return BadRequest("Huỷ duyệt không thành công");
            }
            return NoContent();
        }

    }
}
