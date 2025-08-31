using Service.SnapFood.Manage.Enums;

namespace Service.SnapFood.Manage.Dto.Bill
{
    public class UpdateOrderStatusDto
    {
        public StatusOrder StatusOrder { get; set; }
        public string Reason { get; set; } = string.Empty;
        public string NhanVienThaoTac { get; set; } = string.Empty;

    }
}
