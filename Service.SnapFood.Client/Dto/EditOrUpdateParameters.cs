using Microsoft.AspNetCore.Components;
using Service.SnapFood.Share.Model.ServiceCustomHttpClient;

namespace Service.SnapFood.Client.Dto
{
    public class EditOrUpdateParameters
    {
        public Guid Id { get; set; }
        public bool IsEditMode { get; set; }
        public object? Data { get; set; } = null;
        public ApiRequestModel RequestApi { get; set; } = new ApiRequestModel();
        public EventCallback OnRefresh { get; set; } = new EventCallback();
    }
}
