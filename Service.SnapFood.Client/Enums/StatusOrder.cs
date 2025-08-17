using System.ComponentModel;

namespace Service.SnapFood.Client.Enums
{
    public enum StatusOrder
    {

        [Description("Tất cả trạng thái")]
        None = -1,

        [Description("Chờ xác nhận")]
        Pending = 0,

        //[Description("Đã xác nhận")]
        //Confirmed = 1,

        [Description("Đang chuẩn bị")]
        DangChuanBi = 2,

        [Description("Đang giao hàng")]
        Shipping = 3,

        [Description("Chờ lấy hàng")]
        ChoLayHang = 4,





        [Description("Thành công")]
        Completed = 9,

        [Description("Đã hủy")]
        Cancelled = 10,

    }
}
