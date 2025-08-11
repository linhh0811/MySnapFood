using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.SnapFood.Application.Dtos.ThongKe
{
    public class TTSoLieuTheoThoiGianDto
    {
        public int TongHoaDon { get; set; }
        public int TongHoaDonThanhCong { get; set; }
        public int TongHoaDonBiHuy { get; set; }


        public decimal TienKhuyenMai { get; set; }
        public decimal TienMaGiamGia { get; set; }
        public decimal PhiShip { get; set; }
        public decimal TongDoanhThu { get; set; }
       
    }
}
