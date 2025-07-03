using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.SnapFood.Domain.Enums
{
    public enum PromotionType
    {
        [Description("Chọn phân loại")]
        None = -1,
        [Description("Giảm số tiền")]
        Amount=0,
        [Description("Giảm còn giá cố định")]
        FixedPrice=1
    }
}
