using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using Service.SnapFood.Client.Dto.Auth;
using Service.SnapFood.Client.Dto.Cart;
using Service.SnapFood.Share.Interface.Extentions;
using Service.SnapFood.Share.Model.Commons;
using Service.SnapFood.Share.Model.ServiceCustomHttpClient;
using Service.SnapFood.Client.Components.Layout;

namespace Service.SnapFood.Client.Components.Pages.Cart
{
    public class CheckoutBase : ComponentBase
    {
        [Inject] protected ICallServiceRegistry CallApi { get; set; } = default!;
        [Inject] protected IToastService ToastService { get; set; } = default!;
        [Inject] protected NavigationManager Navigation { get; set; } = default!;
        [Inject] protected NavMenu NavMenu { get; set; } = default!; // Inject NavMenu

        [CascadingParameter] public CurrentUser CurrentUser { get; set; } = new(); // Thêm CascadingParameter để lấy CurrentUser

        protected CartDto Cart { get; set; } = new CartDto();
        protected List<object> CartItems { get; set; } = new List<object>();
        protected AddressDto? Address { get; set; }
        protected List<StoreDto> Stores { get; set; } = new List<StoreDto>();
        protected StoreDto? SelectedStore { get; set; }
        protected string PaymentMethod { get; set; } = "cash";
        protected Guid UserId { get; set; }

        protected void NavigateToHome()
        {
            Navigation.NavigateTo("/");
        }

        protected override async Task OnInitializedAsync()
        {
            if (CurrentUser.UserId == Guid.Empty)
            {
                ToastService.ShowError("Vui lòng đăng nhập để thanh toán.");
                Navigation.NavigateTo("/");
                return;
            }

            UserId = CurrentUser.UserId;
            await LoadCart();
            await LoadAddress();
            await LoadStores();
        }

        protected void SetPaymentMethod(string method)
        {
            PaymentMethod = method;
        }

        protected async Task LoadCart()
        {
            try
            {
                var request = new ApiRequestModel { Endpoint = $"api/cart/{UserId}" };
                var result = await CallApi.Get<CartDto>(request);
                if (result.Status == StatusCode.OK && result.Data != null)
                {
                    Cart = (CartDto)result.Data;
                    Cart.TotalQuantity = Cart.CartProductItems.Sum(p => p.Quantity) + Cart.CartComboItems.Sum(c => c.Quantity);
                    Cart.TotalPrice = Cart.CartProductItems.Sum(p => p.Quantity * p.Price) + Cart.CartComboItems.Sum(c => c.Quantity * c.Price);
                    CartItems = Cart.CartProductItems.Cast<object>().Concat(Cart.CartComboItems).ToList();
                    await NavMenu.RefreshCartItemCount(); // Cập nhật số lượng trong NavMenu
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

        protected async Task LoadAddress()
        {
            try
            {
                var request = new ApiRequestModel { Endpoint = $"api/address/default/{UserId}" };
                var result = await CallApi.Get<AddressDto>(request);
                if (result.Status == StatusCode.OK && result.Data != null)
                {
                    Address = (AddressDto)result.Data;
                }
                else
                {
                    Address = null;
                }
            }
            catch (Exception ex)
            {
                ToastService.ShowError($"Lỗi khi tải địa chỉ: {ex.Message}");
            }
        }

        protected async Task LoadStores()
        {
            try
            {
                var request = new ApiRequestModel { Endpoint = "api/stores" };
                var result = await CallApi.Get<List<StoreDto>>(request);
                if (result.Status == StatusCode.OK && result.Data != null)
                {
                    Stores = (List<StoreDto>)result.Data;
                    SelectedStore = Stores.FirstOrDefault();
                }
                else
                {
                    Stores = new List<StoreDto>();
                }
            }
            catch (Exception ex)
            {
                ToastService.ShowError($"Lỗi khi tải danh sách cửa hàng: {ex.Message}");
            }
        }

        protected async Task HandleCheckOut()
        {
            try
            {
                if (Cart == null || (!Cart.CartProductItems.Any() && !Cart.CartComboItems.Any()))
                {
                    ToastService.ShowError("Giỏ hàng của bạn trống.");
                    return;
                }
                if (Address == null)
                {
                    ToastService.ShowError("Vui lòng thêm địa chỉ nhận hàng.");
                    return;
                }
                if (SelectedStore == null)
                {
                    ToastService.ShowError("Vui lòng chọn cửa hàng.");
                    return;
                }

                var checkOutDto = new CheckOutDto
                {
                    UserId = UserId,
                    AddressId = Address.Id,
                    StoreId = SelectedStore.Id,
                    PaymentMethod = PaymentMethod
                };
                var request = new ApiRequestModel { Endpoint = "api/cart/checkout" };
                var result = await CallApi.Post<object>(request, checkOutDto);
                if (result.Status == StatusCode.OK)
                {
                    ToastService.ShowSuccess("Đặt hàng thành công.");
                    await NavMenu.RefreshCartItemCount(); // Cập nhật số lượng trong NavMenu sau thanh toán
                    Navigation.NavigateTo("/cart");
                }
                else
                {
                    ToastService.ShowError("Đặt hàng thất bại.");
                }
            }
            catch (Exception ex)
            {
                ToastService.ShowError($"Lỗi khi đặt hàng: {ex.Message}");
            }
        }
    }
}