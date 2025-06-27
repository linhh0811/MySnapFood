using System.ComponentModel;

namespace Service.SnapFood.Client.Enums
{
    public enum PromotionType
    {
        [Description("Chọn phân loại")]
        None = -1,
        [Description("Giảm số tiền")]
        Amount = 0,
        [Description("Giảm còn giá cố định")]
        FixedPrice = 1
    }
}
