using Microsoft.FluentUI.AspNetCore.Components;
using Service.SnapFood.Manage.Enums;
using Service.SnapFood.Share.Model.SQL;
using System.ComponentModel.DataAnnotations;

namespace Service.SnapFood.Manage.Dto.DiscountCode
{
    public class DiscountCodeDto
    {
        public Guid Id { get; set; }
        public int Index { get; set; }
        [Required(ErrorMessage = "Mã giảm giá không được để trống")]
        [RegularExpression("^[A-Za-z0-9]+$", ErrorMessage = "Mã giảm giá không được chứa dấu hoặc khoảng trắng")]
        [StringLength(15,MinimumLength =10,ErrorMessage ="Mã giảm giá giới hạn từ 10 tới 15 ký tự")]
        public string Code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        [Required(ErrorMessage = "Giá trị giảm giá không được để trống")]
        [Range(1, int.MaxValue, ErrorMessage = "Giới hạn sử dụng phải lớn hơn 0")]

        public decimal DiscountValue { get; set; }

        public decimal DiscountValueMax { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        [Range(1,int.MaxValue,ErrorMessage = "Giới hạn sử dụng phải lớn hơn 0")]

        public int UsageLimit { get; set; }
        public int UsedCount { get; set; }
        public decimal MinOrderAmount { get; set; }
        public ModerationStatus ModerationStatus { get; set; }
        public DiscountCodeType DiscountCodeType { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastModified { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid LastModifiedBy { get; set; }
        public string CreatedByName { get; set; } = string.Empty;
        public string LastModifiedByName { get; set; } = string.Empty;
    }
}
