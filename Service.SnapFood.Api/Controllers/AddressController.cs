using Microsoft.AspNetCore.Mvc;
using Service.SnapFood.Application.Dtos;
using Service.SnapFood.Application.Interfaces;
using System;
using System.Threading.Tasks;

namespace Service.SnapFood.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;

        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        [HttpGet ("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var addresses = await _addressService.GetAllAsync();
            return Ok(addresses);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var address = await _addressService.GetByIdAsync(id);
            if (address == null)
                return NotFound("Không tìm thấy địa chỉ");
            return Ok(address);
        }
        [HttpGet("GetAddressByUserId/{id}")]
        public IActionResult GetAddressByUserId(Guid id)
        {
            var addresses = _addressService.GetAddressByUserId(id);
            return Ok(addresses);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddressDto item)
        {
            var id = await _addressService.CreateAsync(item);
            return CreatedAtAction(nameof(GetById), new { id }, item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] AddressDto item)
        {
            var result = await _addressService.UpdateAsync(id, item);
            if (!result)
                return BadRequest("Cập nhật không thành công");
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _addressService.DeleteAsync(id);
            if (!result)
                return BadRequest("Xóa không thành công");
            return NoContent();
        }
    }
}