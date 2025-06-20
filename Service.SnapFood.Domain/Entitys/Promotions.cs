using Service.SnapFood.Domain.Enums;
using Service.SnapFood.Share.Model.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.SnapFood.Domain.Entitys
{
    public class Promotions : BaseDomainEntity
    {
        public string PromotionName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public PromotionType PromotionType { get; set; }
        public decimal PromotionValue { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public virtual List<PromotionItem> PromotionItems { get; set; } = null!;

    }
}
