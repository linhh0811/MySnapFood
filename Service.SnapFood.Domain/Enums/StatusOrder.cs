using System.ComponentModel;

namespace Service.SnapFood.Domain.Enums
{
    public enum StatusOrder
    {
        [Description("Tất cả trạng thái")]
        None = -1,

        [Description("Chờ xác nhận")]
        Pending = 0,

        [Description("Đã xác nhận")]
        Confirmed = 1,

        [Description("Đang chuẩn bị")]
        DangChuanBi = 2,

        [Description("Đã chuẩn bị xong")]
        DaChuanBiXong = 3,

        [Description("Đang giao hàng")]
        Shipping = 4,

        [Description("Chờ lấy hàng")]
        ChoLayHang = 5,






        [Description("Thành công")]
        Completed = 9,

        [Description("Đã hủy")]
        Cancelled = 10,

    }
}
