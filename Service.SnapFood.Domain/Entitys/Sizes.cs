using Service.SnapFood.Share.Model.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.SnapFood.Domain.Entitys
{
    public class Sizes : BaseDomainEntity
    {
        public string SizeName { get; set; } = string.Empty;
        public decimal AdditionalPrice { get; set; }
        public int DisplayOrder { get; set; }
        public Guid? ParentId { get; set; }
        public virtual List<Product> Product { get; set; } = null!;
        public virtual Sizes? Parent { get; set; }

    }
}
