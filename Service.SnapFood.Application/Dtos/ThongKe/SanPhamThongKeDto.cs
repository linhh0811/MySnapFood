using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.SnapFood.Application.Dtos.ThongKe
{
    public class SanPhamThongKeDto
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;

        public int Quantity { get; set; }
    }
}
