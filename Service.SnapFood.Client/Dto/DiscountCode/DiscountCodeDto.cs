using Service.SnapFood.Client.Enums;
using Service.SnapFood.Share.Model.SQL;

namespace Service.SnapFood.Client.Dto.DiscountCode
{
    public class DiscountCodeDto
    {
        public Guid Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal DiscountValue { get; set; }
        public decimal DiscountValueMax { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int UsageLimit { get; set; }
        public int UsedCount { get; set; }
        public decimal MinOrderAmount { get; set; }
        public bool IsActive { get; set; }
        public DiscountCodeType DiscountCodeType { get; set; }
        public string ApplyToOrderTypes { get; set; } = string.Empty;
        public ModerationStatus ModerationStatus { get; set; }

    }
}
