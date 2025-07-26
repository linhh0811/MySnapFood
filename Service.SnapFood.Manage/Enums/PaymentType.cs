using System.ComponentModel;

namespace Service.SnapFood.Manage.Enums
{
    public enum PaymentType
    {
        [Description("Ví điện tử Momo")]
        Momo = 0,//chuyển khoản
        [Description("Tiền mặt")]

        Cash = 1,//tiền mặt
    }
}
