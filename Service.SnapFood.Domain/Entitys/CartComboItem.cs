using Service.SnapFood.Share.Model.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.SnapFood.Domain.Entitys
{
    public class CartComboItem : IntermediaryEntity
    {
        public Guid ComboId { get; set; }

        public Guid CartId { get; set; }
        public Guid? SizeId { get; set; }

        public int Quantity { get; set; }
        public virtual Cart Cart { get; set; } = null!;

        public virtual Combo Combo { get; set; } = null!;

        public virtual List<ComboProductItem> ComboProductItems { get; set; } = null!;


    }
}
