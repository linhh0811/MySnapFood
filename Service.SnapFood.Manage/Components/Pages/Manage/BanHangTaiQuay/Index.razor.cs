
using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using Service.SnapFood.Manage.Dto.Cart;
using Service.SnapFood.Manage.Enums;
using Service.SnapFood.Share.Interface.Extentions;
using Service.SnapFood.Share.Model.Commons;
using Service.SnapFood.Share.Model.ServiceCustomHttpClient;

namespace Service.SnapFood.Manage.Components.Pages.Manage.BanHangTaiQuay
{
    public partial class Index
    {
        [CascadingParameter] public CurrentUser CurrentUser { get; set; } = new();
        [Inject] protected ICallServiceRegistry CallApi { get; set; } = default!;
        [Inject] protected IToastService ToastService { get; set; } = default!;
        protected CartDto CartModel { get; set; } = new CartDto();
        protected List<CartDto> Carts { get; set; } = new List<CartDto>();
        public bool IsUpdateQuantity { get; set; } = false;


        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await LoadListCart();
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
                StateHasChanged();
            }
            IsUpdateQuantity = false;

        }

        public async Task DecreaseQuantity(Guid cartItemId, ItemType itemType, int quantity)
        {
            IsUpdateQuantity = true;

            if (quantity > 1)
            {
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
                    StateHasChanged();
                }
            }
            IsUpdateQuantity = false;


        }

        protected async Task RemoveItem(Guid itemId, ItemType itemType)
        {
            try
            {
                var endpoint = itemType == ItemType.Product
                    ? $"api/Cart/Remove/{itemId}"
                    : $"api/Cart/Removecombo/{itemId}";
                var request = new ApiRequestModel { Endpoint = endpoint };
                var result = await CallApi.Delete(request);
                if (result.Status == StatusCode.OK)
                {
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

        protected async Task ThemMoiDonHang()
        {
            try
            {
                var request = new ApiRequestModel { Endpoint = $"api/Cart/AddCartNew/{CurrentUser.UserId}" };

                ResultAPI result = await CallApi.Post<Guid>(request,null);
                if (result.Status == StatusCode.OK)
                {
                    ToastService.ShowSuccess("Thêm giỏ hàng thành công.");
                    await LoadListCart();
                }
                else
                {
                    ToastService.ShowError("Thêm giỏ hàng thất bại.");
                }
            }
            catch (Exception ex)
            {
                ToastService.ShowError("Thêm giỏ hàng thất bại: "+ ex.Message );

            }
        }

        protected async Task LoadListCart()
        {

            try
            {

                var request = new ApiRequestModel { Endpoint = $"api/Cart/ListCart/{CurrentUser.UserId}" };
                var result = await CallApi.Get<List<CartDto>>(request);
                if (result.Status == StatusCode.OK && result.Data != null)
                {
                    Carts = (List<CartDto>)result.Data;
                    StateHasChanged();
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
    }
}