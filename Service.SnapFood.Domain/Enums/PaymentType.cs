using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.SnapFood.Domain.Enums
{
    public enum PaymentType
    {
        Transfer = 0,//chuyển khoản
        Cash = 1,//tiền mặt
        [Description("Chuyển khoản ngân hàng")]
        BankTransfer = 2,
    }
}
