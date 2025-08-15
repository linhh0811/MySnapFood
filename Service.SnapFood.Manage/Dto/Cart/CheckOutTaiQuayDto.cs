using Service.SnapFood.Manage.Enums;

namespace Service.SnapFood.Manage.Dto.Cart
{
    public class CheckOutTaiQuayDto
    {
        public Guid NhanVienId { get; set; }

        public Guid CartId { get; set; }
        public PaymentType PhuongThucThanhToan { get; set; }
        public string GhiChu { get; set; } = string.Empty;
    }
}
