using Microsoft.AspNetCore.Mvc;
using Service.SnapFood.Application.Dtos;
using Service.SnapFood.Application.Interfaces;
using Service.SnapFood.Share.Query;
using System;
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

        [HttpPost("GetPage")]
        public IActionResult GetPage([FromBody] BaseQuery query)
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
                    }).ToList();
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
                return BadRequest("Cập nhật không thành công");
            return NoContent();
        }
    }
}