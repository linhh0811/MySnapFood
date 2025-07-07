using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.SnapFood.Application.Interfaces;
using Service.SnapFood.Domain.Entitys;
using Service.SnapFood.Infrastructure.EF.Contexts;

namespace Service.SnapFood.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillDetailsController : ControllerBase
    {
        private readonly IBillService _billService;

        public BillDetailsController(IBillService billService)
        {
            _billService = billService;
        }
            // GET: api/BillDetails
            [HttpGet]


        // GET: api/BillDetails/5
        // GET: api/BillDetails/{id} → id ở đây là BillId
        [HttpGet("{id}")]
        public async Task<ActionResult<List<BillDetails>>> GetBillDetailsByBillId(Guid id)
        {
            var billDetails = await _billService.GetBillDetailsByBillIdAsync(id); // TRUYỀN billId vào đây

            if (billDetails == null || billDetails.Count == 0)
            {
                return NotFound("Không tìm thấy chi tiết đơn hàng.");
            }

            return Ok(billDetails);
        }


        
    }
}
