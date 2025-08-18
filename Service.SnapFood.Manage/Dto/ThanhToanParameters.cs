using Microsoft.AspNetCore.Components;
using Service.SnapFood.Manage.Enums;

namespace Service.SnapFood.Manage.Dto
{
    public class ThanhToanParameters
    {
        public Guid CartId { get; set; }
        public PaymentType PhuongThucThanhToan { get; set; }
        public EventCallback OnRefresh { get; set; } = new EventCallback();

    }
}
