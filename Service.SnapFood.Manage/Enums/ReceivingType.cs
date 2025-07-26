using System.ComponentModel;

namespace Service.SnapFood.Manage.Enums
{
    public enum ReceivingType
    {
        [Description("Nhận hàng tại quầy")]
        PickUpAtStore = 0,
        [Description("Giao hàng tận nơi")]
        HomeDelivery = 1,
    }
}
