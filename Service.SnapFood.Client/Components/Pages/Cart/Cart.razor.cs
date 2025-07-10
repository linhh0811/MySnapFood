using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using Service.SnapFood.Client.Dto.Auth;
using Service.SnapFood.Client.Dto.Cart;
using Service.SnapFood.Share.Interface.Extentions;
using Service.SnapFood.Share.Model.Commons;
using Service.SnapFood.Share.Model.ServiceCustomHttpClient;
using Service.SnapFood.Client.Components.Layout;
using Service.SnapFood.Client.Enums;
using System.Threading.Tasks;
using Service.SnapFood.Client.Infrastructure.Service;


namespace Service.SnapFood.Client.Components.Pages.Cart
{
    public class CartBase : ComponentBase
    {
        [CascadingParameter] public CurrentUser CurrentUser { get; set; } = new();
        [Inject] protected ICallServiceRegistry CallApi { get; set; } = default!;
        [Inject] protected IToastService ToastService { get; set; } = default!;
        [Inject] protected NavigationManager Navigation { get; set; } = default!;
        [Inject] protected NavMenu NavMenu { get; set; } = default!; // Inject NavMenu
        [Inject] protected SharedStateService SharedService { get; set; } = default!;
        public decimal totalPrice=0;
        public decimal totalPriceEndown = 0;
        public bool IsUpdateQuantity { get; set; } = false;
        protected CartDto CartModel { get; set; } = new CartDto();
        public bool isLoading = true;
        public string PhuongThucNhanHang = "Nhan-Tai-Quay";


        protected override async Task OnInitializedAsync()
        {
            isLoading = true;
            if (CurrentUser.UserId == Guid.Empty)
            {             
                return;
            }
            await LoadCart();
            isLoading = false;
        }

        protected void NavigateToOrderHome()
        {
            Navigation.NavigateTo("/Dat-Hang");
        }

        protected async Task LoadCart()
        {
           
            try
            {

                var request = new ApiRequestModel { Endpoint = $"api/Cart/{CurrentUser.UserId}" };
                var result = await CallApi.Get<CartDto>(request);
                if (result.Status == StatusCode.OK && result.Data != null)
                {
                    CartModel = (CartDto)result.Data;

                    totalPrice = CartModel.CartItems.Sum(p => p.BasePrice*p.Quantity);
                    totalPriceEndown = CartModel.CartItems.Where(x => x.PriceEndown > 0).Sum(p => p.BasePrice*p.Quantity - p.PriceEndown*p.Quantity);
                    StateHasChanged();
                    await SharedService.TriggerUpdateAsync();
                }
                else
                {
                    ToastService.ShowError("Không thể tải giỏ hàng.");
                }
            }
            catch (Exception ex)
            {
                ToastService.ShowError($"Lỗi khi tải giỏ hàng: {ex.Message}");
            }
        }



        protected async Task RemoveItem(Guid itemId, ItemType itemType)
        {
            try
            {
                var endpoint = itemType== ItemType.Product
                    ? $"api/Cart/Remove/{itemId}"
                    : $"api/Cart/Removecombo/{itemId}";
                var request = new ApiRequestModel { Endpoint = endpoint };
                var result = await CallApi.Delete(request);
                if (result.Status == StatusCode.OK)
                {
                    await LoadCart();
                    ToastService.ShowSuccess("Đã xóa mục khỏi giỏ hàng.");
                }
                else
                {
                    ToastService.ShowError("Không thể xóa mục.");
                }
            }
            catch (Exception ex)
            {
                ToastService.ShowError($"Lỗi khi xóa mục: {ex.Message}");
            }
        }

        protected void CheckOut()
        {
            Navigation.NavigateTo($"/Thanh-Toan/{PhuongThucNhanHang}");
        }

        protected async Task ClearCart()
        {
            try
            {
                var request = new ApiRequestModel { Endpoint = $"api/Cart/Clear/{CartModel.Id}" };
                var result = await CallApi.Delete(request);
                if (result.Status == StatusCode.OK)
                {
                    await LoadCart(); 
                    ToastService.ShowSuccess("Đã xóa giỏ hàng.");
                }
                else
                {
                    ToastService.ShowError("Không thể xóa giỏ hàng.");
                }
            }
            catch (Exception ex)
            {
                ToastService.ShowError($"Lỗi khi xóa giỏ hàng: {ex.Message}");
            }
        }

        public async Task IncreaseQuantity(Guid cartItemId, ItemType itemType)
        {
            IsUpdateQuantity = true;
            QuantityInCartDto QuantityInCartDto = new QuantityInCartDto()
            {
                UserId = CurrentUser.UserId,
                ItemId = cartItemId,
                ItemType = itemType,
                QuantityThayDoi = 1
            };
            var request = new ApiRequestModel { Endpoint = $"api/Cart/UpdateQuantity", };

            ResultAPI resultCheck = await CallApi.Put(request, QuantityInCartDto);
            if (resultCheck.Status == StatusCode.OK)
            {
                ToastService.ShowSuccess("Cập nhật thành công");
                await LoadCart();
                StateHasChanged();
            }
            IsUpdateQuantity = false;

        }

        public async Task DecreaseQuantity(Guid cartItemId, ItemType itemType, int quantity)
        {
            IsUpdateQuantity = true;

            if (quantity > 1) {
                QuantityInCartDto QuantityInCartDto = new QuantityInCartDto()
                {
                    UserId = CurrentUser.UserId,
                    ItemId = cartItemId,
                    ItemType = itemType,
                    QuantityThayDoi = -1
                };
                var request = new ApiRequestModel { Endpoint = $"api/Cart/UpdateQuantity" };

                ResultAPI resultCheck = await CallApi.Put(request, QuantityInCartDto);
                if (resultCheck.Status == StatusCode.OK)
                {
                    ToastService.ShowSuccess("Cập nhật thành công");
                    await LoadCart();
                    StateHasChanged();
                }
            }
            IsUpdateQuantity = false;


        }
        public void PhuongThucNhanHangClick(string phuongThuc)
        {
            PhuongThucNhanHang = phuongThuc;
            StateHasChanged();
        }
        public string GetBorderStyle(string value)
        {
            return PhuongThucNhanHang == value ? "2px solid #FF969A" : "2px solid #ccc";
        }
    }
}