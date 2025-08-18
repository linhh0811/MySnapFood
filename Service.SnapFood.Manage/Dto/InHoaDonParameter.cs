using Microsoft.AspNetCore.Components;
using Service.SnapFood.Share.Model.ServiceCustomHttpClient;

namespace Service.SnapFood.Manage.Dto
{
    public class InHoaDonParameter
    {
        public Guid Id { get; set; }
        public bool IsTaiQuay { get; set; } = false;
        public EventCallback OnRefresh { get; set; } = new EventCallback();
    }
}
