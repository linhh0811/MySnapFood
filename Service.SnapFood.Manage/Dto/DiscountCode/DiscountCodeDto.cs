using Service.SnapFood.Manage.Enums;
using Service.SnapFood.Share.Model.SQL;

namespace Service.SnapFood.Manage.Dto.DiscountCode
{
    public class DiscountCodeDto
    {
        public Guid Id { get; set; }
        public int Index { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal DiscountAmount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int UsageLimit { get; set; }
        public int UsedCount { get; set; }
        public decimal MinOrderAmount { get; set; }
        public bool IsActive { get; set; }
        public string ApplyToOrderTypes { get; set; } = string.Empty;
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
