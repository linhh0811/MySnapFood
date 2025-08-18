using Service.SnapFood.Domain.Enums;
using Service.SnapFood.Share.Model.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.SnapFood.Domain.Entitys
{
    public class PromotionItem : IntermediaryEntity
    {
        public Guid PromotionId { get; set; }
        public Guid ItemId { get; set; }
        public Guid? ProductId { get; set; }
        public Guid? ComboId { get; set; }

        public ItemType ItemType { get; set; }
        public int Quantity { get; set; }
        public virtual Product? Product { get; set; }
        public virtual Combo? Combo { get; set; }
        public virtual Promotions Promotion { get; set; } = null!;
    }
}
