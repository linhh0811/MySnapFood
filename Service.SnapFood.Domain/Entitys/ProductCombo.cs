using Service.SnapFood.Share.Model.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.SnapFood.Domain.Entitys
{
    public class ProductCombo : IntermediaryEntity
    {
        public Guid ProductId { get; set; }
        public Guid ComboId { get; set; }
        public int Quantity { get; set; }
        public virtual Product Product { get; set; } = null!;
        public virtual Combo Combo { get; set; } = null!;
    }
}
