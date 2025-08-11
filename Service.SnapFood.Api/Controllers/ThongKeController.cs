using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.SnapFood.Application.Interfaces;
using Service.SnapFood.Application.Service;
using Service.SnapFood.Share.Query;

namespace Service.SnapFood.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThongKeController : ControllerBase
    {
        private readonly IThongKeService _thongKeService;
        public ThongKeController(IThongKeService thongKeService)
        {
            _thongKeService = thongKeService;   
        }
        [HttpGet("GetTTSoLieu")]
        public async Task<IActionResult> GetTTSoLieu()
        {
            var thongke = await _thongKeService.GetTTSoLieu();
            return Ok(thongke);
        }
        [HttpPost("GetTTSoLieuTheoThoiGian")]
        public IActionResult GetTTSoLieuTheoThoiGian(BaseQuery baseQuery)
        {
            var thongke =  _thongKeService.GetTTSoLieuTheoThoiGian(baseQuery);
            return Ok(thongke);
        }

        [HttpPost("GetSanPhamComboCount")] 
        public IActionResult GetSanPhamComboCount(BaseQuery baseQuery)
        {
            var thongke = _thongKeService.GetSanPhamComboDtos(baseQuery);
            return Ok(thongke);
        }

        [HttpPost("GetDoanhThu")] 
        public IActionResult GetDoanhThu(BaseQuery baseQuery)
        {
            var thongke = _thongKeService.GetDoanhThuDtos(baseQuery);
            return Ok(thongke);
        }
    }
}
