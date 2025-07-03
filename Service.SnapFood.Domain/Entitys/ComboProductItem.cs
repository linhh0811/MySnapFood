using Service.SnapFood.Share.Model.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.SnapFood.Domain.Entitys
{
    public class ComboProductItem : IntermediaryEntity
    {
        public Guid CartComboId { get; set; }
        public Guid ProductId { get; set; }
        public Guid? SizeId { get; set; }
        public int Quantity { get; set; }
        public virtual CartComboItem CartComboItem { get; set; } = null!;
        public virtual Sizes Size { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;



    }
}
