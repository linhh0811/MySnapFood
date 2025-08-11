using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using Service.SnapFood.Manage.Dto.Address;
using Service.SnapFood.Manage.Dto.Store;
using Service.SnapFood.Manage.Dto.ThongTinGiaoHang;
using Service.SnapFood.Manage.Enums;
using Service.SnapFood.Manage.Infrastructure.Services;
using Service.SnapFood.Share.Interface.Extentions;
using Service.SnapFood.Share.Model.Commons;
using Service.SnapFood.Share.Model.ServiceCustomHttpClient;
using System.Text.Json.Serialization;
using static System.Net.WebRequestMethods;

namespace Service.SnapFood.Manage.Components.Pages.Manage.CuaHang
{
    public partial class Index
    {
        [CascadingParameter] public CurrentUser CurrentUser { get; set; } = new();
        [Inject] private ICallServiceRegistry CallApi { get; set; } = default!;
        [Inject] private IToastService ToastService { get; set; } = default!;
        private StoreDto Store { get; set; } = new StoreDto();
        private ThongTinGiaoHangDto ThongTinGiaoHang { get; set; } = new ThongTinGiaoHangDto();
        private bool isLoading=false;
        private Dictionary<String, String> TrangThaiCuaHangOptions = new Dictionary<string, string>();
        private string SelectedTrangThai = string.Empty;
        private bool isSavingStore = false;
        private bool isSavingAddress = false;
        private bool isSavingTTGiaoHang = false;



        protected override async Task OnInitializedAsync()
        {
            isLoading = true;
            await LoadProvinces();
            await LoadStores();
            isLoading=false;
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
                    ThongTinGiaoHang = Store.ThongTinGiaoHang ?? new ThongTinGiaoHangDto();
                    AddressModel = Store.Address ?? new AddressDto();
                    var Province = provinces.FirstOrDefault(p => p.Name.ToString() == AddressModel.Province);
                    SelectedProvince = Province?.Code.ToString() ?? string.Empty;
                    await OnProvinceChangedAsync();
                    var District = districts.FirstOrDefault(p => p.Name.ToString() == AddressModel.District);
                    SelectedDistrict = District?.Code.ToString() ?? string.Empty;                   
                    await OnDistrictChangedAsync();
                    var Ward = wards.FirstOrDefault(p => p.Name.ToString() == AddressModel.Ward);
                    SelectedWard = Ward?.Code.ToString() ?? string.Empty;
                    GetPromotionType();
                    SelectedTrangThai = Store.Status.ToString();
                }

            }
            catch (Exception ex)
            {
                ToastService.ShowError($"Lỗi khi tải danh sách cửa hàng: {ex.Message}");
            }
        }

        private void GetPromotionType()
        {
            TrangThaiCuaHangOptions = Enum.GetValues(typeof(Status))
            .Cast<Status>()          
            .ToDictionary(
                e => (e).ToString(),
                e => e.GetDescription()
            );

        }
        #region Update
        private async Task UpdateStore()
        {
          
            try
            {
                isSavingStore = true;

                ApiRequestModel requestRestAPI = new ApiRequestModel();
                requestRestAPI.Endpoint = $"api/Store/{Store.Id}";
                Store.Status = (Status)Enum.Parse(typeof(Status), SelectedTrangThai);
                ResultAPI result = await CallApi.Put(requestRestAPI, Store);
                if (result.Status == StatusCode.OK)
                {
                    ToastService.ShowSuccess("Sửa thông tin cửa hàng thành công.");
                }
                else
                {
                    ToastService.ShowError("Sửa thông tin cửa hàng thất bại: " + result.Message);;
                }
            }
            catch (Exception ex)
            {
                ToastService.ShowError("Sửa thông tin cửa hàng thất bại: " +ex.Message);
            }
            finally
            {
                isSavingStore = false;
            }
        }
        private async Task UpdateAddress()
        {

            try
            {
                isSavingAddress = true;
                ApiRequestModel requestRestAPI = new ApiRequestModel();

                requestRestAPI.Endpoint = $"api/Address/{AddressModel.Id}";
            
                ResultAPI result = await CallApi.Put(requestRestAPI, AddressModel);
                if (result.Status == StatusCode.OK)
                {
                    ToastService.ShowSuccess("Sửa địa chỉ thành công.");
                }
                else
                {

                    ToastService.ShowError("Sửa địa chỉ thất bại: " + result.Message);
                }
            }
            catch (Exception ex)
            {

                ToastService.ShowError("Sửa địa chỉ thất bại: " + ex.Message);

            }
            finally
            {
                isSavingAddress = false;
            }
        }
        private async Task UpdateThongTinGiaoHang()
        {

            try
            {
                isSavingTTGiaoHang = true;
                ApiRequestModel requestRestAPI = new ApiRequestModel();

                requestRestAPI.Endpoint = $"api/ThongTinGiaoHang/{ThongTinGiaoHang.Id}";

                ResultAPI result = await CallApi.Put(requestRestAPI, ThongTinGiaoHang);
                if (result.Status == StatusCode.OK)
                {
                    ToastService.ShowSuccess("Sửa thông tin giao hàng thành công.");
                }
                else
                {

                    ToastService.ShowError("Sửa thông tin giao hàng thất bại: " + result.Message);
                }
            }
            catch (Exception ex)
            {

                ToastService.ShowError("Sửa thông tin giao hàng thất bại: " + ex.Message);

            }
            finally
            {
                isSavingTTGiaoHang = false;
            }
        }
        #endregion
        #region địa chỉ     
        private List<ProvinceResponse> provinces = new();
        private List<District> districts = new();
        private List<Ward> wards = new();
        private string SelectedProvince = string.Empty;
        private string SelectedDistrict = string.Empty;
        private string SelectedWard = string.Empty;
        private AddressDto AddressModel { get; set; } = new AddressDto();


        private async Task LoadProvinces()
        {
            provinces = await Http.GetFromJsonAsync<List<ProvinceResponse>>("https://provinces.open-api.vn/api/?depth=2") ?? new();
        }

        private async Task OnProvinceChangedAsync()
        {
            try
            {
                SelectedDistrict = string.Empty;
                SelectedWard = string.Empty;

                districts = new List<District>();

                if (!string.IsNullOrEmpty(SelectedProvince))
                {
                    // Lấy toàn bộ response trước
                    var response = await Http.GetFromJsonAsync<ProvinceResponse>(
                        $"https://provinces.open-api.vn/api/p/{SelectedProvince}?depth=2");

                    // Lấy danh sách districts từ response
                    districts = response?.Districts ?? new List<District>();

                    var selectedProvince = provinces.FirstOrDefault(p => p.Code.ToString() == SelectedProvince);
                    AddressModel.Province = selectedProvince?.Name ?? string.Empty;
                }
                else
                {
                    AddressModel.Province = string.Empty;
                    AddressModel.District = string.Empty;
                    AddressModel.Ward = string.Empty;
                }
            }
            catch (Exception ex)
            {
                ToastService.ShowError("Lỗi tải danh sách tỉnh/thành phố: "+ ex.Message);
     
            }


        }

        private async Task OnDistrictChangedAsync()
        {
            // Reset giá trị phường/xã
            SelectedWard = string.Empty;

            if (!string.IsNullOrEmpty(SelectedDistrict))
            {

                var response = await Http.GetFromJsonAsync<District>(
                     $"https://provinces.open-api.vn/api/d/{SelectedDistrict}?depth=2");

                // Lấy danh sách districts từ response
                wards = response?.Wards ?? new List<Ward>();
                var selectedDistrict = districts.FirstOrDefault(p => p.Code.ToString() == SelectedDistrict);
                AddressModel.District = selectedDistrict?.Name ?? string.Empty;
            }
            else
            {
                wards = new List<Ward>();
                AddressModel.District = string.Empty;
                AddressModel.Ward = string.Empty;
            }

            StateHasChanged();
        }
        private void OnWardChangedAsync()
        {
            
            if (!string.IsNullOrEmpty(SelectedWard))
            {          
                var selectedWard = wards.FirstOrDefault(p => p.Code.ToString() == SelectedWard);
                AddressModel.Ward = selectedWard?.Name ?? string.Empty;
            }
            else
            {
                AddressModel.Ward = string.Empty;
            }


            StateHasChanged();
        }
        public class ProvinceResponse
        {
            [JsonPropertyName("name")]
            public string Name { get; set; } = string.Empty;

            [JsonPropertyName("code")]
            public int Code { get; set; }

            [JsonPropertyName("districts")]
            public List<District> Districts { get; set; } = new();
        }

        public class District
        {
            [JsonPropertyName("name")]
            public string Name { get; set; } = string.Empty;

            [JsonPropertyName("code")]
            public int Code { get; set; }

            [JsonPropertyName("wards")]
            public List<Ward> Wards { get; set; } = new();
        }

        public class Ward
        {
            [JsonPropertyName("name")]
            public string Name { get; set; } = string.Empty;

            [JsonPropertyName("code")]
            public int Code { get; set; }
        }
        #endregion
    }
}
