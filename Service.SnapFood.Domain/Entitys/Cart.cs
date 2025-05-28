using Service.SnapFood.Share.Model.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.SnapFood.Domain.Entitys
{
    public class Cart : IntermediaryEntity
    {
        public Guid UserId { get; set; }
        public virtual User User { get; set; } = null!;
        public virtual List<CartProductItem> CartProductItems { get; set; } = null!;
        public virtual List<CartComboItem> CartComboItems { get; set; } = null!;

    }
}
