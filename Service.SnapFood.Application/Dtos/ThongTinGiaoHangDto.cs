using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.SnapFood.Application.Dtos
{
    public class ThongTinGiaoHangDto
    {
        public Guid Id { get; set; }
        public double BanKinhGiaoHang { get; set; }//km
        public decimal PhiGiaoHang { get; set; }
        public decimal DonHangToiThieu { get; set; }
    }
}
