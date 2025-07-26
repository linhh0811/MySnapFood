using Microsoft.AspNetCore.Mvc;
using Service.SnapFood.Application.Dtos;
using Service.SnapFood.Application.Interfaces;
using Service.SnapFood.Domain.Enums;
using Service.SnapFood.Domain.Query;
using Service.SnapFood.Share.Query;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Service.SnapFood.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillController : ControllerBase
    {
        private readonly IBillService _billService;

        public BillController(IBillService billService)
        {
            _billService = billService;
        }

        [HttpPost("GetPaged")]
        public IActionResult GetPage( BillQuery query)
        {
            var result = _billService.GetPage(query);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var bills = await _billService.GetAllAsync();
            return Ok(bills);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var bill = await _billService.GetByIdAsync(id);
            return Ok(bill);
        }

        [HttpGet("ByUser/{userId}")]
        public async Task<IActionResult> GetByUser(Guid userId)
        {
            try
            {
                var bills = await _billService.GetAllAsync();
                var userBills = bills.Where(b => b.UserId == userId)
                    .Select(b => new BillDto
                    {
                        Id = b.Id,
                        BillCode = b.BillCode,
                        UserId = b.UserId,
                        StoreId = b.StoreId,
                        Status = b.Status,
                        TotalAmount = b.TotalAmount,
                        TotalAmountEndow = b.TotalAmountEndow,
                        Created = b.Created
                    }).OrderByDescending(x => x.Created).ToList();
                return Ok(userBills);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BillDto item)
        {
            var id = await _billService.CreateAsync(item);
            return CreatedAtAction(nameof(GetById), new { id }, item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] BillDto item)
        {
            var result = await _billService.UpdateAsync(id, item);
            if (!result)
                return BadRequest();
            return NoContent();
        }

        [HttpPut("UpdateStatus/{id}")]
        public async Task<IActionResult> UpdateStatus(Guid id, [FromBody] UpdateOrderStatusDto updateOrderStatusDto)
        {

                var result = await _billService.UpdateStatusAsync(id, updateOrderStatusDto);
                if (!result)
                    return BadRequest();

                return Ok();
           
        }

        [HttpGet("DetailsByBillId/{billId}")]
        public async Task<IActionResult> GetDetailsByBillId(Guid billId)
        {
            try
            {
                var result = await _billService.GetBillDetailsByBillIdAsync(billId);
                if (result == null || !result.Any())
                    return NotFound("Không tìm thấy chi tiết đơn hàng");

                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest($"Lỗi: {ex.Message}");
            }
        }


      

        [HttpGet("total")]
        public async Task<IActionResult> GetTotalRevenue()
        {
            var totalRevenue = await _billService.GetTotalRevenueAsync();
            return Ok(totalRevenue);
        }

        [HttpGet("totalInvoices")]
        public async Task<IActionResult> GetTotalInvoices()
        {
            var totalInvoices = await _billService.GetTotalInvoicesAsync();
            return Ok(totalInvoices);
        }


        [HttpGet("total-cancelled")]
        public async Task<IActionResult> GetTotalCancelledOrders()
        {
            var count = await _billService.GetCancelledOrdersCountAsync();
            return Ok(count);
        }


        [HttpGet("dailyRevenue")]
        public async Task<IActionResult> GetDailyRevenue([FromQuery] DateTime fromDate, [FromQuery] DateTime toDate)
        {
            if (fromDate > toDate)
                return BadRequest("Ngày bắt đầu phải nhỏ hơn hoặc bằng ngày kết thúc.");

            var data = await _billService.GetDailyRevenueAsync(fromDate, toDate);
            return Ok(data);
        }


   

        [HttpGet("weeklyRevenue")]
        public async Task<IActionResult> GetWeeklyRevenue()
        {
            var data = await _billService.GetWeeklyRevenueAsync();
            return Ok(data);
        }

        [HttpGet("monthlyRevenue")]
        public async Task<IActionResult> GetMonthlyRevenue()
        {
            var data = await _billService.GetMonthlyRevenueAsync();
            return Ok(data);
        }

        [HttpGet("top-best-selling")]
        public async Task<IActionResult> GetTopBestSellingProducts([FromQuery] int top = 5)
        {
            var data = await _billService.GetTopBestSellingProductsAsync( top);
            return Ok(data);
        }

        [HttpGet("top-best-combo")]
        public async Task<IActionResult> GetTopBestSellingCombos([FromQuery] int top = 5)
        {
            
            var data = await _billService.GetTopBestSellingCombosAsync(top);
            return Ok(data);
        }


        [HttpPost("ApplyDiscount")]
        public async Task<IActionResult> ApplyDiscount([FromQuery] Guid billId, [FromQuery] Guid discountCodeId, [FromQuery] Guid userId)
        {
            try
            {
                var result = await _billService.ApplyDiscountAsync(billId, discountCodeId, userId);
                if (!result)
                    return BadRequest("Không thể áp dụng mã giảm giá");


                return Ok("Áp dụng mã giảm giá thành công");
            }
            catch (Exception ex)
            {
                return BadRequest($"Lỗi: {ex.Message}");
            }
        }
    }
}
