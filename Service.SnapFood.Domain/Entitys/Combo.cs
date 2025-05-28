using Service.SnapFood.Share.Model.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.SnapFood.Domain.Entitys
{
    public class Combo : BaseDomainEntity
    {
        public Guid CategoryId { get; set; }
        public string ComboName { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public decimal BasePrice { get; set; }
        public string? Description { get; set; }
        public int Quantity { get; set; }
        public virtual Categories Category { get; set; } = null!;
        public virtual List<ProductCombo> ProductComboes { get; set; } = null!;
        public virtual List<CartComboItem> CartItemes { get; set; } = null!;
    }
}
