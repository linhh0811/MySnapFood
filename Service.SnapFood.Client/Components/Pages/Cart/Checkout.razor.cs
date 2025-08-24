using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using Service.SnapFood.Client.Components.Layout;
using Service.SnapFood.Client.Components.Pages.DiscountCode;
using Service.SnapFood.Client.Dto;
using Service.SnapFood.Client.Dto.Addresss;
using Service.SnapFood.Client.Dto.Auth;
using Service.SnapFood.Client.Dto.Cart;
using Service.SnapFood.Client.Dto.DiscountCode;
using Service.SnapFood.Client.Dto.Momo;
using Service.SnapFood.Client.Dto.ThongTinGiaoHang;
using Service.SnapFood.Client.Enums;
using Service.SnapFood.Client.Infrastructure.Service;
using Service.SnapFood.Share.Interface.Extentions;
using Service.SnapFood.Share.Model.Commons;
using Service.SnapFood.Share.Model.Momo;
using Service.SnapFood.Share.Model.ServiceCustomHttpClient;
using Service.SnapFood.Share.Model.SQL;
using System.Text.Json;
using static Service.SnapFood.Client.Infrastructure.Service.AddressService;

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
        [Inject] protected IAddressService AddressService { get; set; } = default!; // Inject NavMenu
        [Inject] protected SharedStateService SharedService { get; set; } = default!;
        [Inject] private IDialogService DialogService { get; set; } = default!;

        protected CartDto CartModel { get; set; } = new CartDto();

        protected List<object> CartItems { get; set; } = new List<object>();
        protected AddressDto Address { get; set; } = new AddressDto();
        protected StoreDto Store { get; set; } = new StoreDto();
        private ThongTinGiaoHangDto ThongTinGiaoHang = new ThongTinGiaoHangDto(); 
        protected DiscountCodeDto DiscountCode = new DiscountCodeDto();
        protected PaymentType SelectedPaymentMethod { get; set; } = PaymentType.Cash;
        protected decimal totalPrice = 0;
        protected decimal totalPriceEndown = 0;
        protected bool isLoading = true;
        protected string Notes { get; set; } = string.Empty;
        protected string ReceiverName { get; set; } = string.Empty;
        protected string ReceiverPhone { get; set; } = string.Empty;
        protected double KhoangCach = 0;
        protected decimal PhiVanChuyen = 0;
        private Guid SelectedDiscountCode;

       protected decimal DiscountCodeValue { get; set; }

        protected void NavigateToHome()
        {
            Navigation.NavigateTo("/");
        }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                if (CurrentUser.UserId == Guid.Empty)
                {
                    Navigation.NavigateTo("/");
                }
                else
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
                    await LoadThongTinGiaoHang();

                    isLoading = false;
                }
            }
            catch (Exception)
            {
                isLoading = false;

            }

        }

        protected async Task LoadThongTinGiaoHang()
        {

            try
            {

                var request = new ApiRequestModel { Endpoint = $"api/ThongTinGiaoHang" };
                var result = await CallApi.Get<ThongTinGiaoHangDto>(request);
                if (result.Status == StatusCode.OK && result.Data != null)
                {
                    ThongTinGiaoHang = (ThongTinGiaoHangDto)result.Data;

                    DistanceRequest DistanceRequest = new DistanceRequest
                    {
                        OriginLatitude = Store.Address.Latitude,
                        OriginLongitude = Store.Address.Longitude,
                        DestinationLatitude = Address.Latitude,
                        DestinationLongitude = Address.Longitude
                    };

                    if(PhuongThucNhanHang== "Giao-Tan-Noi")
                    {
                        KhoangCach = await AddressService.CalculateDistanceKmAsync(DistanceRequest) ?? 0;

                        PhiVanChuyen = (decimal)KhoangCach * ThongTinGiaoHang.PhiGiaoHang;
                    }
                   


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
               
               
                var checkOutDto = new CheckOutDto
                {
                    UserId = CurrentUser.UserId,
                    Description = Notes,
                };
                if (PhuongThucNhanHang == "Nhan-Tai-Quay")
                {
                    checkOutDto.ReceivingType = ReceivingType.PickUpAtStore;
                    checkOutDto.PaymentType = SelectedPaymentMethod;
                    checkOutDto.ReceiverName = ReceiverName;
                    checkOutDto.ReceiverPhone = ReceiverPhone;
                }
                else
                {
                    checkOutDto.ReceivingType = ReceivingType.HomeDelivery;
                    checkOutDto.PaymentType = SelectedPaymentMethod;
                    checkOutDto.PhiGiaoHang = PhiVanChuyen;
                    checkOutDto.KhoangCach = KhoangCach;



                }
                checkOutDto.DiscountCodeId = SelectedDiscountCode;
                checkOutDto.DiscountCodeValue = DiscountCodeValue;
                checkOutDto.TongTienKhuyenMai = totalPriceEndown;

                var request = new ApiRequestModel { Endpoint = "api/Cart/Checkout" };
                var result = await CallApi.Post<object>(request, checkOutDto);
                if (result.Status == StatusCode.OK)
                {

                    Navigation.NavigateTo("/");
                    ToastService.ShowSuccess("Đặt hàng thành công.");
                    await SharedService.TriggerUpdateAsync();
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

        protected async Task<bool> KiemTraCheckOut()
        {
            try
            {


                var checkOutDto = new CheckOutDto
                {
                    UserId = CurrentUser.UserId,
                    Description = Notes,
                };
                if (PhuongThucNhanHang == "Nhan-Tai-Quay")
                {
                    checkOutDto.ReceivingType = ReceivingType.PickUpAtStore;
                    checkOutDto.PaymentType = SelectedPaymentMethod;
                    checkOutDto.ReceiverName = ReceiverName;
                    checkOutDto.ReceiverPhone = ReceiverPhone;
                }
                else
                {
                    checkOutDto.ReceivingType = ReceivingType.HomeDelivery;
                    checkOutDto.PaymentType = SelectedPaymentMethod;
                    checkOutDto.PhiGiaoHang = PhiVanChuyen;
                    checkOutDto.KhoangCach = KhoangCach;



                }
                checkOutDto.DiscountCodeId = SelectedDiscountCode;
                checkOutDto.DiscountCodeValue = DiscountCodeValue;
                checkOutDto.TongTienKhuyenMai = totalPriceEndown;

                var request = new ApiRequestModel { Endpoint = "api/Cart/KiemTraCheckout" };
                var result = await CallApi.Post<object>(request, checkOutDto);
                if (result.Status == StatusCode.OK)
                {

                    return true;
                }
                else
                {
                    ToastService.ShowError("Đặt hàng thất bại." + result.Message);
                    return false;
                }
            }
            catch (Exception ex)
            {
                ToastService.ShowError($"Lỗi khi đặt hàng: {ex.Message}");
                return false;

            }
        }
        protected async Task OpenModalDiscountCode()
        {
            try
            {
                var data = new DiscountParameter()
                {
                    Price = totalPrice - totalPriceEndown,
                    Id=DiscountCode.Id
                };


                var dialog = await DialogService.ShowDialogAsync<View>(data,new DialogParameters
                {
                    PreventDismissOnOverlayClick = true,
                    Title = "Mã giảm giá",
                    ShowDismiss = false,
                    PreventScroll = true,
                    Modal = true,
                    Width = "700px",
                });
                var result = await dialog.Result;
                if (!result.Cancelled && result.Data is Guid selectedGuid)
                {
                    SelectedDiscountCode = selectedGuid;
                    if (SelectedDiscountCode != Guid.Empty)
                    {
                        DiscountCode = await GetDiscountCode();
                        if (DiscountCode.DiscountCodeType == DiscountCodeType.Money)
                        {
                            var value = DiscountCode.DiscountValue;
                            if (value > (totalPrice - totalPriceEndown))
                            {
                                DiscountCodeValue = totalPrice - totalPriceEndown;
                            }
                            else
                            {
                                DiscountCodeValue = value;
                            }

                        } else if (DiscountCode.DiscountCodeType == DiscountCodeType.Percent)
                        {
                            var value = (totalPrice - totalPriceEndown) * DiscountCode.DiscountValue / 100;
                            if (value > DiscountCode.DiscountValueMax)
                            {
                                DiscountCodeValue = DiscountCode.DiscountValueMax;
                            }
                            else
                            {
                                DiscountCodeValue = value;
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                ToastService.ShowError($"Lỗi khi mở modal thêm combo: {ex.Message}");
            }
        }

        protected async Task<DiscountCodeDto> GetDiscountCode()
        {
            var request = new ApiRequestModel { Endpoint = $"api/DiscountCode/DiscountHoatDong/{SelectedDiscountCode}" };
            ResultAPI result = await CallApi.Get<DiscountCodeDto>(request);
            if (result.Status == StatusCode.OK)
            {
                return result.Data as DiscountCodeDto ??new();
            }
            else
            {
                ToastService.ShowWarning("Mã giảm giá đã hết hạn!");
                return new();

            }
        }
        protected void SelectedPayment(PaymentType PhuongThuc)
        {
            SelectedPaymentMethod = PhuongThuc;
        }

        private async Task OpenModalQrCk()
        {
            try
            {
                var maCk = Guid.NewGuid().ToString("N").Substring(0, 5).ToUpper();
                var parameters = new QRCKParameter
                {
                    GiaTriDonHang = totalPrice - totalPriceEndown + PhiVanChuyen - DiscountCodeValue,
                    NoiDungChuyenKhoan = $"SnapFodd - Mã: {maCk}",
                    MaCK = maCk

                };
                var dialog = await DialogService.ShowDialogAsync<ViewQRCK>(parameters, new DialogParameters
                {
                    Title = "QRCK",
                    PreventDismissOnOverlayClick = true,
                    PreventScroll = true,
                    Modal = true
                });
                var result = await dialog.Result;
                if (!result.Cancelled)
                {
                    await HandleCheckOut();
                }
            }
            catch (Exception ex)
            {
                ToastService.ShowError($"Lỗi khi mở modal thêm sản phẩm: {ex.Message}");
            }
        }

        protected async Task HandleCheckOut2()
        {
            if (Store.Status==Status.InActivity)
            {
                ToastService.ShowError("Cửa hàng đang ngừng hoạt động.");
                return;
            }

            if (Store.ThoiGianBatDauHoatDong>DateTime.Now||Store.ThoiGianNgungHoatDong<=DateTime.Now)
            {
                ToastService.ShowError($"Cửa hàng hoạt động từ {Store.ThoiGianBatDauHoatDong:HH\\:mm} tới {Store.ThoiGianNgungHoatDong:HH\\:mm}.");

                return;
            }
            if (PhuongThucNhanHang == "Nhan-Tai-Quay")
            {
                if (string.IsNullOrEmpty(ReceiverPhone)||string.IsNullOrEmpty(ReceiverName) )
                {
                    ToastService.ShowError("Vui lòng nhập đầy đủ thông tin.");
                    return;
                }
            }
            else
            {
                if (string.IsNullOrEmpty(Address.NumberPhone))
                {
                    ToastService.ShowError("Vui lòng thêm địa chỉ nhận hàng.");
                    return;
                }
            }
                

            if ((totalPrice - totalPriceEndown + PhiVanChuyen - DiscountCodeValue) < ThongTinGiaoHang.DonHangToiThieu)
            {
                ToastService.ShowError($"Giá trị đơn hàng nhỏ hơn giá trị tối thiểu là: {ThongTinGiaoHang.DonHangToiThieu.ToString("N0")} đ");
                return;
            }

            if (KhoangCach > ThongTinGiaoHang.BanKinhGiaoHang && PhuongThucNhanHang == "Giao-Tan-Noi")
            {
                ToastService.ShowError($"Khoảng cách giao hàng vượt quá bán kính giao hàng({ThongTinGiaoHang.BanKinhGiaoHang} km).");
                return;
            }

            if (CartModel.CartItems.Count(x=>x.ModerationStatus==ModerationStatus.Rejected)>0)
            {
                ToastService.ShowWarning($"Vui lòng xóa sản phẩm hết hàng ra khỏi giỏ hàng");
                return;
            }

            var check = await KiemTraCheckOut();
            if (check)
            {
                if (SelectedPaymentMethod == PaymentType.BankTransfer)
                {
                    await OpenModalQrCk();
                }
                else if (SelectedPaymentMethod == PaymentType.Cash)
                {
                    await HandleCheckOut();
                }
            }
           
        }
    }
}