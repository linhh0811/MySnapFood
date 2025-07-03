using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using Service.SnapFood.Client.Dto.Auth;
using Service.SnapFood.Client.Dto.Cart;
using Service.SnapFood.Share.Interface.Extentions;
using Service.SnapFood.Share.Model.Commons;
using Service.SnapFood.Share.Model.ServiceCustomHttpClient;
using Service.SnapFood.Client.Components.Layout;
using Service.SnapFood.Client.Enums;

namespace Service.SnapFood.Client.Components.Pages.Cart
{
    public class CheckoutBase : ComponentBase
    {
        [CascadingParameter] public CurrentUser CurrentUser { get; set; } = new();
        [Parameter] public string PhuongThucNhanHang { get; set; } = string.Empty;
        [Inject] protected ICallServiceRegistry CallApi { get; set; } = default!;
        [Inject] protected IToastService ToastService { get; set; } = default!;
        [Inject] protected NavigationManager Navigation { get; set; } = default!;
        [Inject] protected NavMenu NavMenu { get; set; } = default!; // Inject NavMenu
        protected CartDto CartModel { get; set; } = new CartDto();

        protected List<object> CartItems { get; set; } = new List<object>();
        protected AddressDto Address { get; set; }= new AddressDto();
        protected StoreDto Store { get; set; } = new StoreDto();
        protected StoreDto? SelectedStore { get; set; }
        protected string PaymentMethod { get; set; } = "cash";
        public decimal totalPrice = 0;
        public decimal totalPriceEndown = 0;
        public bool isLoading = true;
        public string Notes { get; set; } = string.Empty;
        public string ReceiverName { get; set; } = string.Empty;
        public string ReceiverPhone { get; set; } = string.Empty;

        protected void NavigateToHome()
        {
            Navigation.NavigateTo("/");
        }

        protected override async Task OnInitializedAsync()
        {
            isLoading = true;
            if (CurrentUser.UserId == Guid.Empty)
            {
                ToastService.ShowError("Vui lòng đăng nhập để thanh toán.");
                Navigation.NavigateTo("/");
                return;
            }
            await LoadCart();
            await LoadAddress();
            await LoadStores();
            isLoading = false;
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

                    totalPrice = CartModel.CartItems.Sum(p => p.BasePrice * p.Quantity);
                    totalPriceEndown = CartModel.CartItems.Where(x => x.PriceEndown > 0).Sum(p => p.BasePrice * p.Quantity - p.PriceEndown * p.Quantity);
                    StateHasChanged();
                    await NavMenu.RefreshCartItemCount();
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
                var request = new ApiRequestModel { Endpoint = $"api/Cart/Address/{CurrentUser.UserId}" };
                var result = await CallApi.Get<AddressDto>(request);
                if (result.Status == StatusCode.OK && result.Data != null)
                {
                    Address = (AddressDto)result.Data;
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
                var request = new ApiRequestModel { Endpoint = "api/Store/GetStore" };
                var result = await CallApi.Get<StoreDto>(request);
                if (result.Status == StatusCode.OK && result.Data != null)
                {
                    Store = result.Data as StoreDto ?? new StoreDto();
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
                if (string.IsNullOrEmpty(Address.NumberPhone))
                {
                    ToastService.ShowError("Vui lòng thêm địa chỉ nhận hàng.");
                    return;
                }

                var checkOutDto = new CheckOutDto
                {
                    UserId = CurrentUser.UserId,
                    Description = Notes,
                };
                if (PhuongThucNhanHang== "Nhan-Tai-Quay")
                {
                    checkOutDto.ReceivingType = ReceivingType.PickUpAtStore;
                    checkOutDto.PaymentType = PaymentType.Cash;

                }
                else
                {
                    checkOutDto.ReceivingType = ReceivingType.HomeDelivery;
                    checkOutDto.PaymentType = PaymentType.Cash;
                    checkOutDto.ReceiverName = ReceiverName;
                    checkOutDto.ReceiverPhone = ReceiverPhone;
                }


                    var request = new ApiRequestModel { Endpoint = "api/Cart/Checkout" };
                var result = await CallApi.Post<object>(request, checkOutDto);
                if (result.Status == StatusCode.OK)
                {
                    
                    Navigation.NavigateTo("/");
                    ToastService.ShowSuccess("Đặt hàng thành công.");
                }
                else
                {
                    ToastService.ShowError("Đặt hàng thất bại." + result.Message);
                }
            }
            catch (Exception ex)
            {
                ToastService.ShowError($"Lỗi khi đặt hàng: {ex.Message}");
            }
        }
    }
}