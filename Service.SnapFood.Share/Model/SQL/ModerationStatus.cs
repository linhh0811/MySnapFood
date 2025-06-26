using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.SnapFood.Share.Model.SQL
{
    public enum ModerationStatus
    {
        [Description("Chọn trạng thái")]
        None = -1,

        [Description("Đã duyệt")]
        Approved = 0,

        [Description("Chưa phê duyệt")]
        Rejected = 2,

       
    }
}
