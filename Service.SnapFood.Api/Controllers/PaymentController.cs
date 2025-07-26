using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using Service.SnapFood.Application.Interfaces;
using Service.SnapFood.Infrastructure.Service;
using Service.SnapFood.Share.Model;
using Service.SnapFood.Share.Model.Momo;
using System.ComponentModel.Design;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Service.SnapFood.Infrastructure.Controllers
{
    [Route("api/payment")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IMomoService _momoService;

        public PaymentController(IMomoService momoService)
        {
            _momoService = momoService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreatePayment([FromBody] OrderInfoModel model)
        {
            var response = await _momoService.CreatePaymentAsync(model);
            if (response.ErrorCode == 0)
            {   
                return Ok(new { PayUrl = response.PayUrl });
            }
            return BadRequest(new { Message = response.Message });
        }

       

        [HttpGet("momo/callback")]
        public IActionResult MomoCallback([FromQuery] MomoExecuteResponseModel model)
        {
            var response = _momoService.PaymentExecuteAsync(HttpContext.Request.Query);
            if (model.ResultCode == 0) // Thanh toán thành công
            {
                // Đơn hàng đã được tạo ở NotifyUrl, chỉ cần redirect
                return Redirect("https://localhost:7199");
            }
            else
            {
                // Thanh toán thất bại, redirect về giỏ hàng với thông báo lỗi
                return Redirect("/Gio-Hang?error=Giao dịch không thành công");
            }
        }
    }
}