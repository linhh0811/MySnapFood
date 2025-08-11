using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.SnapFood.Application.Dtos.Footer
{
    public class FooterDto
    {
        public string TenCuaHang { get; set; } = string.Empty;

        public string DiaChi { get; set; } = string.Empty;
        public string DienThoai { get; set; } = string.Empty;
        public string ThoiGianHoatDong { get; set; } = string.Empty;

        public decimal PhiGiaoHangKm { get; set; }
        public double GiaoHangToiDa { get; set; }
        public decimal GiaTriDonHang { get; set; }
    }
}
