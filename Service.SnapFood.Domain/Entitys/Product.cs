using Service.SnapFood.Share.Model.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.SnapFood.Domain.Entitys
{
    public class Product : BaseDomainEntity
    {
        public Guid CategoryId { get; set; }
        public Guid SizeId { get; set; }
        public string ImageUrl { get; set; } = string.Empty;

        public string ProductName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int Quantity { get; set; }
        public decimal BasePrice { get; set; }

        public virtual Categories Category { get; set; } = null!;
        public virtual Sizes Size { get; set; } = null!;

        public virtual List<CartItem> CartItemes { get; set; } = null!;

        public virtual List<ProductCombo> ProductComboes { get; set; } = null!;
    }
}
