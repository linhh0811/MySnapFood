using System.ComponentModel;

namespace Service.SnapFood.Manage.Enums
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
