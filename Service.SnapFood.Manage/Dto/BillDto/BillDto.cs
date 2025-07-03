using Service.SnapFood.Manage.Enums;
using System.ComponentModel.DataAnnotations;

namespace Service.SnapFood.Manage.Dto.BillDto
{
    public class BillDto
    {
        public int Index { get; set; }
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Mã hóa đơn không được để trống.")]
        [StringLength(50, ErrorMessage = "Mã hóa đơn không được vượt quá 50 ký tự.")]
        public string BillCode { get; set; } = string.Empty;

        [Required(ErrorMessage = "Người dùng là bắt buộc.")]
        public Guid UserId { get; set; }

        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Cửa hàng là bắt buộc.")]
        public Guid StoreId { get; set; }

        public string StoreName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Trạng thái đơn hàng là bắt buộc.")]
        public StatusOrder Status { get; set; }

        public string StatusText { get; set; } = string.Empty;

        [Range(0, double.MaxValue, ErrorMessage = "Tổng tiền phải lớn hơn hoặc bằng 0.")]
        public decimal TotalAmount { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Tiền khuyến mãi phải lớn hơn hoặc bằng 0.")]
        public decimal TotalAmountEndow { get; set; }

        [Required(ErrorMessage = "Ngày tạo là bắt buộc.")]
        public DateTime Created { get; set; } 
    }
}
