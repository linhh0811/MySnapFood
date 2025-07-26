using System.ComponentModel;

namespace Service.SnapFood.Manage.Enums
{
    public enum Status
    {
        [Description("Hoạt động")]
        Activity = 0,

        [Description("Ngừng hoạt động")]
        InActivity = 1,
    }
}
