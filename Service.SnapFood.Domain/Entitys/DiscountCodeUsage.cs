using Service.SnapFood.Share.Model.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.SnapFood.Domain.Entitys
{
    public class DiscountCodeUsage : IntermediaryEntity
    {
        public Guid DiscountCodeId { get; set; }
        public Guid UserId { get; set; }
        public Guid BillId { get; set; }
        public DateTime UsedAt { get; set; }

        // Navigation
        public virtual DiscountCode DiscountCode { get; set; } = null!;
        public virtual User User { get; set; } = null!;
        public virtual Bill Bill { get; set; } = null!;
    }
}
