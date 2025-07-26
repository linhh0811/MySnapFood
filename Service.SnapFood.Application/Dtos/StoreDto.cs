using Service.SnapFood.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.SnapFood.Application.Dtos
{
    public class StoreDto
    {
        public Guid Id { get; set; }
        public string StoreName { get; set; } = string.Empty;
        public AddressDto Address { get; set; } = new AddressDto();
        public ThongTinGiaoHangDto ThongTinGiaoHang { get; set; } = new ThongTinGiaoHangDto();

        public Status Status { get; set; }
        public DateTime? ThoiGianBatDauHoatDong { get; set; }
        public DateTime? ThoiGianNgungHoatDong { get; set; }
        public string NumberPhone { get; set; } = string.Empty;

    }
}
