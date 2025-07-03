using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using Service.SnapFood.Client.Dto;
using Service.SnapFood.Client.Dto.Addresss;
using Service.SnapFood.Share.Interface.Extentions;
using Service.SnapFood.Share.Model.Commons;
using Service.SnapFood.Share.Model.ServiceCustomHttpClient;

//using Microsoft.AspNetCore.Components;
//using Microsoft.AspNetCore.Components.Web;
//using Microsoft.FluentUI.AspNetCore.Components;
////using Service.SnapFood.Manage.Components.Pages.Manage.Product;
////using Service.SnapFood.Manage.Components.Share;
////using Service.SnapFood.Manage.Dto;
////using Service.SnapFood.Manage.Dto.ProductDto;
//using Service.SnapFood.Share.Interface.Extentions;
//using Service.SnapFood.Share.Model.ServiceCustomHttpClient;
//using Service.SnapFood.Share.Query;
//using Service.SnapFood.Share.Query.Grid;
//using System.Text.Json;

namespace Service.SnapFood.Client.Components.Pages.Addresss
{
    public partial class Index : ComponentBase
    {
        // ──────────────────────────────── DI
        [Inject] private ICallServiceRegistry CallApi { get; set; } = default!;
        [Inject] private IToastService ToastService { get; set; } = default!;
        [Inject] private IDialogService DialogService { get; set; } = default!;

        // ──────────────────────────────── Grid / UI state
        protected FluentDataGrid<AddresssDto>? AddressGrid;
        private List<AddresssDto> addressList;
        private readonly PaginationState pagination = new() { ItemsPerPage = 10 };
        protected string? SearchKeyword { get; set; } = string.Empty;

        [CascadingParameter] public CurrentUser CurrentUser { get; set; } = new();

        private readonly ApiRequestModel requestRestAPI = new();

        // ──────────────────────────────── Data provider for FluentDataGrid
        private async ValueTask<GridItemsProviderResult<AddresssDto>> LoadAddress(GridItemsProviderRequest<AddresssDto> request)
        {
            try
            {
                requestRestAPI.Endpoint = "api/address/getall";
                var result = await CallApi.Get<List<AddresssDto>>(requestRestAPI);
                if (result.Status != StatusCode.OK || result.Data is not List<AddresssDto> allItems)
                {
                    ToastService.ShowWarning(result.Message ?? "Không thể tải danh sách địa chỉ.");
                    return GridItemsProviderResult.From<AddresssDto>(new List<AddresssDto>(), 0);
                }

                // Lọc theo người dùng hiện tại
                IEnumerable<AddresssDto> query = allItems.Where(a => a.UserId == CurrentUser.UserId);


            }


            catch (Exception ex)
            {
                ToastService.ShowError($"Lỗi khi tải địa chỉ: {ex.Message}");
                return GridItemsProviderResult.From<AddresssDto>(new List<AddresssDto>(), 0);
            }
            return GridItemsProviderResult.From<AddresssDto>(new List<AddresssDto>(), 0);
        }




        // ──────────────────────────────── Refresh helpers
        private async Task RefreshDataAsync() => await AddressGrid?.RefreshDataAsync()!;
        private async Task RefreshDataAsync(string? _) => await RefreshDataAsync();
        private async Task RefreshDataAsync(int pageSize)
        {
            pagination.ItemsPerPage = pageSize;
            await pagination.SetCurrentPageIndexAsync(0);
        }

        // ──────────────────────────────── Modal helpers
        private void OpenModalAdd() => ShowEditDialog(isEdit: false);
        private void OpenModalUpdate(Guid id) => ShowEditDialog(isEdit: true, id);

        private async void ShowEditDialog(bool isEdit, Guid id = default)
        {
            try
            {
                var parameters = new EditOrUpdateParameters
                {
                    Id = id,
                    IsEditMode = isEdit,
                    OnRefresh = EventCallback.Factory.Create(this, RefreshDataAsync)
                };

                await DialogService.ShowDialogAsync<Edit>(parameters, new DialogParameters
                {
                    Title = isEdit ? "Sửa địa chỉ" : "Thêm địa chỉ",
                    PreventDismissOnOverlayClick = true,
                    PreventScroll = true,
                    Modal = true
                });
            }
            catch (Exception ex)
            {
                ToastService.ShowError($"Lỗi khi mở modal {(isEdit ? "sửa" : "thêm")}: {ex.Message}");
            }
        }

        private async Task SetDefaultAsync(Guid id)
        {
            try
            {
                requestRestAPI.Endpoint = $"api/address/{id}/SetDefault";
                var result = await CallApi.Put(requestRestAPI, new object());
                if (result.Status == StatusCode.OK)
                {
                    ToastService.ShowSuccess("Đã đặt địa chỉ mặc định.");
                    await RefreshDataAsync();
                }
                else ToastService.ShowWarning(result.Message ?? "Thao tác thất bại.");
            }
            catch (Exception ex)
            {
                ToastService.ShowError("Lỗi: " + ex.Message);
            }
        }

        private async Task DeleteAsync(Guid id)
        {
            if (id == Guid.Empty) return;
            try
            {
                requestRestAPI.Endpoint = $"api/address/{id}";
                var result = await CallApi.Delete(requestRestAPI);
                if (result.Status == StatusCode.OK)
                {
                    ToastService.ShowSuccess("Đã xóa địa chỉ.");
                    await RefreshDataAsync();
                }
                else ToastService.ShowWarning(result.Message ?? "Xóa thất bại.");
            }
            catch (Exception ex)
            {
                ToastService.ShowError("Xóa thất bại: " + ex.Message);
            }
        }
    }
}
