using Service.SnapFood.Manage.Dto.Address;
using Service.SnapFood.Manage.Dto.ThongTinGiaoHang;
using Service.SnapFood.Manage.Enums;

namespace Service.SnapFood.Manage.Dto.Store
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
