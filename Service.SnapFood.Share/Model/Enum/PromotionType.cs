using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.SnapFood.Share.Model.Enum
{
    public enum PromotionType
    {
        [Description("Giảm số tiền")]
        Amount,

        [Description("Giảm còn giá cố định")]
        FixedPrice

    }
}
