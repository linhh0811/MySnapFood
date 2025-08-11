using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.SnapFood.Application.Dtos.ThongKe
{
    public class BieuDoSanPhamComboDto
    {
        public List<SanPhamComboDto> SanPhamComboCount { get; set; } = new();
        public List<SanPhamThongKeDto> SanPhamThongKe { get; set; } = new();
        public List<ComboThongKeDto> ComboThongKeDto { get; set; } = new();
    }
}
