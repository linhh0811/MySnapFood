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
        [Description("Đã duyệt")]
        Approved = 0,

        [Description("Chờ duyệt")]
        Pending = 1,

        [Description("Từ chối")]
        Rejected = 2,

        [Description("Chờ xem xét")]
        PendingReview = 3,

        [Description("Chờ phê duyệt")]
        PendingApproval = 4,

        [Description("Nháp")]
        Draft = 5,

        [Description("Hủy hoặc Xóa")]
        Cancelled = 6
    }
}
