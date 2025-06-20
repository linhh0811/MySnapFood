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
    public class CartBase : ComponentBase
    {
        [Inject] protected ICallServiceRegistry CallApi { get; set; } = default!;
        [Inject] protected IToastService ToastService { get; set; } = default!;
        [Inject] protected NavigationManager Navigation { get; set; } = default!;
        [Inject] protected NavMenu NavMenu { get; set; } = default!; // Inject NavMenu

        [CascadingParameter] public CurrentUser CurrentUser { get; set; } = new();

        protected CartDto Cart { get; set; } = new CartDto();
        protected List<CartItem> CartItems { get; set; } = new List<CartItem>();
        protected Guid UserId { get; set; }

        public class CartItem
        {
            public Guid Id { get; set; }
            public string Name { get; set; } = string.Empty;
            public string SizeName { get; set; } = string.Empty;
            public decimal Price { get; set; }
            public int Quantity { get; set; }
            public string ImageUrl { get; set; } = string.Empty;
        }

        public class CartProductItem : CartItem { }
        public class CartComboItem : CartItem { }
        private CancellationTokenSource _cts = new();

       

        public void Dispose()
        {
            _cts.Cancel();
            _cts.Dispose();
        }

        protected override async Task OnInitializedAsync()
        {
            if (CurrentUser.UserId == Guid.Empty)
            {
                ToastService.ShowError("Vui lòng đăng nhập để xem giỏ hàng.");
                Navigation.NavigateTo("/");
                return;
            }

            UserId = CurrentUser.UserId;
            try
            {
                await LoadCart(_cts.Token);
            }
            catch (OperationCanceledException) { }
        }
        protected void NavigateToOrderHome()
        {
            Navigation.NavigateTo("/Order-Home");
        }

        protected async Task LoadCart(CancellationToken token)
        {
            try
            {
                var request = new ApiRequestModel { Endpoint = $"api/cart/{UserId}" };
                var result = await CallApi.Get<CartDto>(request).WaitAsync(token);
                if (!token.IsCancellationRequested && result.Status == StatusCode.OK && result.Data != null)
                {
                    Cart = (CartDto)result.Data;
                    CartItems = Cart.CartProductItems.Select(p => new CartItem
                    {
                        Id = p.Id,
                        Name = p.ProductName,
                        SizeName = p.SizeName,
                        Price = p.Price,
                        Quantity = p.Quantity,
                        ImageUrl = p.ImageUrl
                    }).Concat(Cart.CartComboItems.Select(c => new CartItem
                    {
                        Id = c.Id,
                        Name = c.ComboName,
                        SizeName = c.SizeName,
                        Price = c.Price,
                        Quantity = c.Quantity,
                        ImageUrl = c.ImageUrl
                    })).ToList();
                    StateHasChanged();
                    await NavMenu.RefreshCartItemCount();
                }
                else if (!token.IsCancellationRequested)
                {
                    ToastService.ShowError("Không thể tải giỏ hàng.");
                }
            }
            catch (OperationCanceledException) { }
            catch (Exception ex)
            {
                ToastService.ShowError($"Lỗi khi tải giỏ hàng: {ex.Message}");
            }
        }
        protected async Task RemoveItem(CartItem item)
        {
            try
            {
                var request = new ApiRequestModel { Endpoint = $"api/cart/remove/{item.Id}" };
                var result = await CallApi.Delete(request);
                if (result.Status == StatusCode.OK)
                {
                    await LoadCart(_cts.Token); // Tải lại giỏ hàng và cập nhật NavMenu trong LoadCart
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

        protected async Task UpdateQuantity(CartItem item)
        {
            try
            {
                var request = new ApiRequestModel { Endpoint = $"api/cart/update/{item.Id}" };
                var result = await CallApi.Put(request, item.Quantity);
                if (result.Status == StatusCode.OK)
                {
                    await LoadCart(_cts.Token); // Tải lại giỏ hàng và cập nhật NavMenu trong LoadCart
                    ToastService.ShowSuccess("Đã cập nhật số lượng.");
                }
                else
                {
                    ToastService.ShowError("Không thể cập nhật số lượng.");
                }
            }
            catch (Exception ex)
            {
                ToastService.ShowError($"Lỗi khi cập nhật số lượng: {ex.Message}");
            }
        }

        protected void CheckOut()
        {
            Navigation.NavigateTo("/checkout");
        }

        protected async Task ClearCart()
        {
            try
            {
                var request = new ApiRequestModel { Endpoint = $"api/cart/clear/{Cart.Id}" };
                var result = await CallApi.Delete(request);
                if (result.Status == StatusCode.OK)
                {
                    await LoadCart(_cts.Token); // Tải lại giỏ hàng và cập nhật NavMenu trong LoadCart
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
    }
}