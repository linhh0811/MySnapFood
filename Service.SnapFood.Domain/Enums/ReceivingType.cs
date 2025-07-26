using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.SnapFood.Domain.Enums
{
    public enum ReceivingType
    {
        [Description("Nhận hàng tại quầy")]
        PickUpAtStore = 0,
        [Description("Giao hàng tận nơi")]
        HomeDelivery = 1,
    }
}
