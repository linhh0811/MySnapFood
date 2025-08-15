
using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using Service.SnapFood.Manage.Components.Share;
using Service.SnapFood.Manage.Dto;
using Service.SnapFood.Manage.Dto.Cart;
using Service.SnapFood.Manage.Enums;
using Service.SnapFood.Share.Interface.Extentions;
using Service.SnapFood.Share.Model.Commons;
using Service.SnapFood.Share.Model.ServiceCustomHttpClient;
using Service.SnapFood.Share.Query.Grid;
using Service.SnapFood.Share.Query;
using System.Threading.Tasks;
using Service.SnapFood.Share.Model.SQL;
using System.Text.Json;

namespace Service.SnapFood.Manage.Components.Pages.Manage.BanHangTaiQuay
{
    public partial class Index
    {
        [CascadingParameter] public CurrentUser CurrentUser { get; set; } = new();
        [Inject] protected ICallServiceRegistry CallApi { get; set; } = default!;
        [Inject] protected IToastService ToastService { get; set; } = default!;
        [Inject] private IDialogService DialogService { get; set; } = default!;
        protected CartDto CartModel { get; set; } = new CartDto();
        protected List<CartDto> Carts { get; set; } = new List<CartDto>();
        public bool IsUpdateQuantity { get; set; } = false;
        public bool IsLoading { get; set; } = true;
        public decimal totalPrice = 0;
        public decimal totalPriceEndown = 0;
        public Guid CartSelect { get; set; }
        public PaymentType PhuongThucThanhToan { get; set; } = PaymentType.Cash;
        private string searchText = string.Empty;


        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                IsLoading = true;
                await LoadListCart();


                IsLoading = false;
                StateHasChanged();

            }
        }

        protected async Task LoadCart()
        {

            try
            {
                if (CartSelect != Guid.Empty)
                {
                    var request = new ApiRequestModel { Endpoint = $"api/Cart/CartId/{CartSelect}" };
                    var result = await CallApi.Get<CartDto>(request);
                    if (result.Status == StatusCode.OK && result.Data != null)
                    {
                        CartModel = (CartDto)result.Data;

                        totalPrice = CartModel.CartItems.Sum(p => p.BasePrice * p.Quantity);
                        totalPriceEndown = CartModel.CartItems.Where(x => x.PriceEndown > 0).Sum(p => p.BasePrice * p.Quantity - p.PriceEndown * p.Quantity);
                        StateHasChanged();
                    }
                    else
                    {
                        ToastService.ShowError("Không thể tải giỏ hàng.");
                    }
                }
                else
                {
                    CartModel = new CartDto();
                }

            }
            catch (Exception ex)
            {
                ToastService.ShowError($"Lỗi khi tải giỏ hàng: {ex.Message}");
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
                    await LoadCart();
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
                    await LoadCart();
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

                ResultAPI result = await CallApi.Post<Guid>(request, null);
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
                ToastService.ShowError("Thêm giỏ hàng thất bại: " + ex.Message);

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
                    if (Carts.Count() > 0)
                    {
                        CartSelect = Carts.First().Id;
                    }
                    else
                    {
                        CartSelect = Guid.Empty;
                    }
                    await LoadCart();
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

        protected async Task DeleteAsync(Guid id)
        {
            try
            {
                var dialog = await DialogService.ShowDialogAsync<ModalConfirm>(new DialogParameters());
                var resultDialog = await dialog.Result;
                if (resultDialog.Cancelled == false && resultDialog.Data is bool success && success)
                {
                    var request = new ApiRequestModel { Endpoint = $"api/Cart/RemoveCart/{id}" };

                    ResultAPI result = await CallApi.Delete(request);
                    if (result.Status == StatusCode.OK)
                    {
                        ToastService.ShowSuccess("Xóa hóa đơn chờ thành công");
                        await LoadListCart();
                        if (Carts.Count() > 0)
                        {
                            CartSelect = Carts.Last().Id;
                        }
                        CartSelect = Carts.First().Id;

                        await LoadCart();
                        StateHasChanged();
                    }
                    else
                    {
                        ToastService.ShowError("Xóa hóa đơn chờ thất bại: " + result.Message);

                    }
                }
            }
            catch (Exception ex)
            {
                ToastService.ShowError("Xóa thất bại: " + ex.Message);
            }

        }

        private async Task DanhSachSanPham()
        {
            try
            {
                var parameters = new EditOrUpdateParameters
                {
                    Id = CartSelect,
                    OnRefresh = EventCallback.Factory.Create(this, LoadCart)
                };
                var dialog = await DialogService.ShowDialogAsync<DanhSachSanPham>(parameters, new DialogParameters
                {
                    Title = "Danh sách sản phẩm / combo",
                    PreventDismissOnOverlayClick = true,
                    PreventScroll = true,
                    Modal = true,
                    Width = "1200px",
                    TrapFocus = false
                });

                // Chờ modal đóng
                var result = await dialog.Result;

                // Gọi LoadCart() sau khi modal đóng
                await LoadCart();


            }
            catch (Exception ex)
            {
                ToastService.ShowError($"Lỗi khi mở modal chi tiết: {ex.Message}");
            }
        }

        private async Task OpenView()
        {
            if (CartSelect == Guid.Empty)
            {
                ToastService.ShowWarning($"Chưa có đơn hàng");

            }
            else if (CartModel.CartItems.Count() == 0)
            {
                ToastService.ShowWarning($"Đơn hàng trống");

            }
            else
            {
                try
                {
                    var parameters = new ThanhToanParameters
                    {
                        CartId = CartSelect,
                        PhuongThucThanhToan = PhuongThucThanhToan,
                        OnRefresh = EventCallback.Factory.Create(this, LoadListCart)
                    };
                    await DialogService.ShowDialogAsync<View>(parameters, new DialogParameters
                    {
                        Title = "XÁC NHẬN ĐƠN HÀNG",
                        PreventDismissOnOverlayClick = true,
                        PreventScroll = true,
                        Modal = true,
                        Width = "600px",
                        TrapFocus = false

                    });
                }
                catch (Exception ex)
                {
                    ToastService.ShowError($"Lỗi khi mở modal chi tiết: {ex.Message}");
                }
            }

        }

        public async Task SelectCart(Guid CartId)
        {
            CartSelect = CartId;
            await LoadCart();

            StateHasChanged();
        }

        private async Task OnSearchSanPhamSelect(OptionsSearchEventArgs<ComboKetHopProductDto> e)
        {
            string input = e.Text;
            e.Items = await FetchDataDichVu(input);
        }
        private async Task<List<ComboKetHopProductDto>> FetchDataDichVu(string searchKeyword)
        {
            var output = new List<ComboKetHopProductDto>();

            try
            {
                ApiRequestModel apiRequest = new ApiRequestModel()
                {

                    Endpoint = "api/Product/GetPageProductCombo"

                };
                var baseQuery = new BaseQuery
                {
                    draw = 1,
                    Keyword = searchKeyword,
                    gridRequest = new GridRequest
                    {
                        filter = new Filter(),
                        page = 1,
                        pageSize = int.MaxValue,

                    }
                };
                ResultAPI result = await CallApi.Post<Dto.DataTableJson>(apiRequest, baseQuery);
                if (result.Status == StatusCode.OK && result.Data is Dto.DataTableJson dataTable)
                {
                    var items = JsonSerializer.Deserialize<List<ComboKetHopProductDto>>(dataTable.Data.GetRawText(),
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<ComboKetHopProductDto>();


                    output = items.ToList();

                }
            }
            catch (Exception ex)
            {

                ToastService.ShowError($"Lỗi khi lấy dữ liệu: {ex.Message}");

            }

            return output;

        }

        public async Task OnDoubleClick(ComboKetHopProductDto product)
        {
            try
            {
                if (CartSelect != Guid.Empty)
                {
                    if (product.ItemType == ItemType.Product)
                    {
                        if (!string.IsNullOrEmpty(product.SizeName))
                        {
                            await OpenModalProductAdd(product.Id);
                        }
                        else
                        {
                            await AddProductToCart(product.Id);
                        }
                    }
                    else if (product.ItemType == ItemType.Combo)
                    {
                        await OpenModalComboAdd(product.Id);
                    }
                    
                }
                else
                {
                    ToastService.ShowWarning($"Vui lòng tạo giỏ hàng");

                }
            }
            catch (Exception ex)
            {

                ToastService.ShowError($"Lỗi thêm sản phẩm vào giỏ hàng: " + ex.Message);

            }
            finally
            {
                await LoadCart();
            }



        }

        private async Task AddProductToCart(Guid productId)
        {
            try
            {
                ApiRequestModel apiRequest = new ApiRequestModel()
                {

                    Endpoint = "api/Cart/AddProductToCart"

                };

                AddProductToCartDto AddProductToCartDto = new AddProductToCartDto
                {
                    ProductId = productId,
                    CartId = CartSelect,
                    Quantity = 1
                };
                var result = await CallApi.Post<object>(apiRequest, AddProductToCartDto);

                // Xử lý kết quả từ API
                if (result.Status == StatusCode.OK)
                {
                    ToastService.ShowSuccess("Đã thêm sản phẩm vào giỏ hàng.");

                }
                else
                {
                    ToastService.ShowError($"Không thể thêm sản phẩm: {result.Message}");
                }
            }
            catch (Exception ex)
            {
                ToastService.ShowError($"Lỗi: {ex.Message}");
            }
        }


        private async Task OpenModalProductAdd(Guid id)
        {
            try
            {
                var parameters = new ViewParameters
                {
                    Id = id,
                    CartId = CartSelect,
                    OnRefresh = EventCallback.Factory.Create(this, LoadCart)
                };
                var dialog = await DialogService.ShowDialogAsync<Product.View>(parameters, new DialogParameters
                {
                    Title = "Thêm sản phẩm",
                    PreventDismissOnOverlayClick = true,
                    PreventScroll = true,
                    Modal = true,
                    Width = "800px",
                    ShowTitle = false,
                    ShowDismiss = false

                });
            }
            catch (Exception ex)
            {
                ToastService.ShowError($"Lỗi khi mở modal thêm sản phẩm: {ex.Message}");
            }
        }

        private async Task OpenModalComboAdd(Guid id)
        {
            try
            {
                var parameters = new ViewParameters
                {
                    Id = id,
                    CartId = CartSelect,
                    OnRefresh = EventCallback.Factory.Create(this, LoadCart)
                };
                var dialog = await DialogService.ShowDialogAsync<Combo.View>(parameters, new DialogParameters
                {
                    Title = "Thêm Combo",
                    PreventDismissOnOverlayClick = true,
                    PreventScroll = true,
                    Modal = true,
                    Width = "900px",
                    ShowTitle = false,
                    ShowDismiss = false

                });
            }
            catch (Exception ex)
            {
                ToastService.ShowError($"Lỗi khi mở modal thêm sản phẩm: {ex.Message}");
            }
        }
    }
}