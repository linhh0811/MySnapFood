using Service.SnapFood.Domain.Enums;
using Service.SnapFood.Share.Model.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Azure.Core.HttpHeader;

namespace Service.SnapFood.Domain.Entitys
{
    public class CartProductItem : IntermediaryEntity
    {
        public Guid ProductId { get; set; }

        public Guid CartId { get; set; }
        public Guid SizeId { get; set; }

        public int Quantity { get; set; }
        public virtual Cart Cart { get; set; } = null!;

        public virtual Product Product { get; set; } = null!;

        public virtual Sizes Size { get; set; } = null!;


    }
}
