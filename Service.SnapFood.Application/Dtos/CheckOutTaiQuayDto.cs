using Service.SnapFood.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.SnapFood.Application.Dtos
{
    public class CheckOutTaiQuayDto
    {
        public Guid NhanVienId { get; set; }

        public Guid CartId { get; set; }
        public PaymentType PhuongThucThanhToan { get; set; }
        public string GhiChu { get; set; } = string.Empty;
        public decimal TongTienKhuyenMai { get; set; }

    }
}
