using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.SnapFood.Application.Dtos;
using Service.SnapFood.Application.Interfaces;
using Service.SnapFood.Application.Service;

namespace Service.SnapFood.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly IStoreService _storeService;

        public StoreController(IStoreService storeService)
        {
            _storeService = storeService;
        }

        [HttpGet("GetStore")]
        public async Task<IActionResult> GetStore()
        {
            var store = await _storeService.GetStore();
            return Ok(store);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] StoreDto item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            var result = await _storeService.UpdateAsync(id, item);
            if (result)
            {
                return Ok();
            }
            return BadRequest();


        }
    }
}
