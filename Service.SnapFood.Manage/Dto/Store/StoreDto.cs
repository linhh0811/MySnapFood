using Service.SnapFood.Manage.Dto.Address;
using Service.SnapFood.Manage.Dto.ThongTinGiaoHang;
using Service.SnapFood.Manage.Enums;
using System.ComponentModel.DataAnnotations;

namespace Service.SnapFood.Manage.Dto.Store
{
    public class StoreDto
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Tên cửa hàng không được để trống.")]
        public string StoreName { get; set; } = string.Empty;
        public AddressDto Address { get; set; } = new AddressDto();
        public ThongTinGiaoHangDto ThongTinGiaoHang { get; set; } = new ThongTinGiaoHangDto();

        public Status Status { get; set; }
        public DateTime? ThoiGianBatDauHoatDong { get; set; }
        public DateTime? ThoiGianNgungHoatDong { get; set; }
        [Required(ErrorMessage = "Số điện thoại không được để trống.")]
        [RegularExpression(@"^(0[2-9][0-9]{8,9})$", ErrorMessage = "Số điện thoại không hợp lệ.")]

        public string NumberPhone { get; set; } = string.Empty;


    }
}
