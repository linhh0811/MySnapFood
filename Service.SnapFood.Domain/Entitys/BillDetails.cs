using Service.SnapFood.Domain.Enums;
using Service.SnapFood.Share.Model.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.SnapFood.Domain.Entitys
{
    public class BillDetails : IntermediaryEntity
    {
        public Guid BillId { get; set; }
        public Guid ItemId { get; set; }

        public ItemType ItemType { get; set; }
        public string ItemsName { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;

        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal PriceEndow { get; set; }

        public virtual Bill Bill { get; set; } = null!;
        public virtual List<ComboItemsArchive> ComboItemsArchives { get; set; } = null!;
    }
}
