using Microsoft.AspNetCore.Components;
using Service.SnapFood.Share.Model.ServiceCustomHttpClient;

namespace Service.SnapFood.Client.Dto
{
    public class ViewParameters
    {
        public Guid Id { get; set; }
        public ApiRequestModel RequestApi { get; set; } = new ApiRequestModel();
        public EventCallback OnRefresh { get; set; } = new EventCallback();
    }
}
