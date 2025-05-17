using Service.SnapFood.Share.Model.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.SnapFood.Domain.Entitys
{
    public class Categories : BaseDomainEntity
    {
        public string CategoryName { get; set; } = string.Empty;
        public string ImageUrl { get; set; }= string.Empty;
        public virtual List<Product> Product { get; set; } = null!;
        public virtual List<Combo> Combo { get; set; } = null!;

    }
}
