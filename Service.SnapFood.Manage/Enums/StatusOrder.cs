using System.ComponentModel;

namespace Service.SnapFood.Manage.Enums
{
    public enum StatusOrder
    {
        [Description("Tất cả trạng thái")]
        None = -1,

        [Description("Chờ xác nhận")]
        Pending = 0,

        [Description("Đã xác nhận")]
        Confirmed = 1,

        [Description("Đang giao hàng")]
        Shipping = 2,

        [Description("Đã giao thành công")]
        Completed = 3,

        [Description("Đã hủy")]
        Cancelled = 4,

     
    }
}
