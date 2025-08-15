using Microsoft.AspNetCore.Components;
using Service.SnapFood.Share.Model.ServiceCustomHttpClient;

namespace Service.SnapFood.Manage.Dto
{
    public class ViewParameters
    {
        public Guid Id { get; set; }
        public Guid CartId { get; set; }

        public ApiRequestModel RequestApi { get; set; } = new ApiRequestModel();
        public EventCallback OnRefresh { get; set; } = new EventCallback();
    }
}
