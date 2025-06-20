using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.SnapFood.Share.Model.Enum
{
    public enum ItemType
    {
        [Description("Sản phẩm")]
        Product,
        [Description("Combo")]
        Combo
    }
}
