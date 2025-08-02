using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.SnapFood.Application.Dtos;
using Service.SnapFood.Application.Interfaces;

namespace Service.SnapFood.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThongTinGiaoHangController : ControllerBase
    {
        private readonly IThongTinGiaoHangService _thongTinGiaoHangService;
        public ThongTinGiaoHangController(IThongTinGiaoHangService thongTinGiaoHangService)
        {
            _thongTinGiaoHangService = thongTinGiaoHangService;
        }
        [HttpGet]
        public IActionResult GetDuLieu()
        {
         
            var result =  _thongTinGiaoHangService.GetDuLieu();           
            return Ok(result);


        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] ThongTinGiaoHangDto item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            
            var result = await _thongTinGiaoHangService.UpdateAsync(id, item);
            if (result)
            {
                return Ok();
            }
            return BadRequest();


        }
    }
}
