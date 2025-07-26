using Service.SnapFood.Domain.Enums;
using Service.SnapFood.Share.Model.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.SnapFood.Domain.Entitys
{
    public class DiscountCode : BaseDomainEntity
    {
        public string Code { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal DiscountAmount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int UsageLimit { get; set; }
        public int UsedCount { get; set; }
        public decimal MinOrderAmount { get; set; }
        public DiscountCodeType DiscountCodeType { get; set; }
        public bool IsActive { get; set; }
        public string? ApplyToOrderTypes { get; set; }

        // Navigation
        public virtual List<DiscountCodeUsage> DiscountCodeUsages { get; set; } = new();
    }
}
