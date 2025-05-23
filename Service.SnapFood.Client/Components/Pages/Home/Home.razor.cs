using Microsoft.AspNetCore.Components;

namespace Service.SnapFood.Client.Components.Pages.Home
{
    public partial class Home : ComponentBase
    {
        public class SlideModel
        {
            public string imgurl { get; set; } = string.Empty;
        }
        List<SlideModel> slides = new List<SlideModel>();
        protected override Task OnInitializedAsync()
        {
            slides = new List<SlideModel>
            {
                new SlideModel { imgurl = "https://inan2h.vn/wp-content/uploads/2022/12/in-banner-quang-cao-do-an-10-1.jpg" },
                new SlideModel { imgurl = "https://inan2h.vn/wp-content/uploads/2022/12/in-banner-quang-cao-do-an-11-1.jpg" },
                new SlideModel { imgurl = "https://inan2h.vn/wp-content/uploads/2022/12/in-banner-quang-cao-do-an-7-1.jpg" }
            };
            return Task.CompletedTask;
        }
    }
}
