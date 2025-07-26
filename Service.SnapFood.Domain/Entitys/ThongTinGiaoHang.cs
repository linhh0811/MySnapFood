using Service.SnapFood.Share.Model.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.SnapFood.Domain.Entitys
{
    public class ThongTinGiaoHang : IntermediaryEntity
    {
        public double BanKinhGiaoHang { get; set; }//km
        public decimal PhiGiaoHang { get; set; }
        public decimal DonHangToiThieu { get; set; }

    }
}
