using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.SnapFood.Application.Interfaces;

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
    }
}
