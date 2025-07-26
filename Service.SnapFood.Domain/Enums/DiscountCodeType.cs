using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Service.SnapFood.Domain.Enums
{
    public enum DiscountCodeType
    {
        [Description("Chọn phân loại")]
        None = -1,
        [Description("Giảm số tiền")]
        Money = 0,
        [Description("Giảm theo phần trăm")]
        Percent = 1
    }
}
