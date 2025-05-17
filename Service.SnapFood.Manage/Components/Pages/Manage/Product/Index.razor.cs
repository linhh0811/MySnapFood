using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.FluentUI.AspNetCore.Components;
using Service.SnapFood.Manage.Components.Share;
using Service.SnapFood.Manage.Dto;
using Service.SnapFood.Manage.Dto.ProductDto;
using Service.SnapFood.Share.Interface.Extentions;
using Service.SnapFood.Share.Model.ServiceCustomHttpClient;
using Service.SnapFood.Share.Query;
using Service.SnapFood.Share.Query.Grid;
using System.Text.Json;
namespace Service.SnapFood.Manage.Components.Pages.Manage.Product
{
    public partial class Index : ComponentBase
    {
        [Inject] private ICallServiceRegistry CallApi { get; set; } = default!;
        [Inject] private IToastService ToastService { get; set; } = default!;
        [Inject] private IDialogService DialogService { get; set; } = default!;
        private ApiRequestModel requestRestAPI = new ApiRequestModel();
        protected FluentDataGrid<ProductDto> ProductGrid { get; set; } = default!;
        protected string? SearchKeyword { get; set; } = string.Empty;
        private bool IsLoadingSync { get; set; } = false;
        private PaginationState pagination = new PaginationState { ItemsPerPage = 10 };
        private async ValueTask<GridItemsProviderResult<ProductDto>> LoadProduct(GridItemsProviderRequest<ProductDto> request)
        {
            try
            {
                var baseQuery = new BaseQuery
                {
                    gridRequest = new GridRequest
                    {
                        page = (request.StartIndex / pagination.ItemsPerPage) + 1,
                        pageSize = pagination.ItemsPerPage,
                        skip = request.StartIndex,
                        take = pagination.ItemsPerPage,
                        sort = request.GetSortByProperties()
                             .Select(s => new Sort
                             {
                                 field = s.PropertyName,
                                 dir = s.Direction == SortDirection.Ascending ? "asc" : "desc"
                             }).ToList()
                    }
                };
                requestRestAPI.Endpoint = "api/Product/GetPaged";

                ResultAPI result = await CallApi.Post<DataTableJson>(requestRestAPI, baseQuery);
                if (result.Status == StatusCode.OK && result.Data is DataTableJson dataTable)
                {
                    var items = JsonSerializer.Deserialize<List<ProductDto>>(dataTable.Data.GetRawText(),
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<ProductDto>(); ;



                    var totalRecords = dataTable.RecordsTotal ?? items.Count;
                    return GridItemsProviderResult.From(items, totalRecords);
                }
                StateHasChanged();
                return GridItemsProviderResult.From(new List<ProductDto>(), 0);
            }
            catch (Exception ex)
            {
                ToastService.ShowError($"Lỗi khi tải danh sách sản phẩm: {ex.Message}");
                return GridItemsProviderResult.From(new List<ProductDto>(), 0);
            }

        }
        private async Task RefresData(int value)
        {
            try
            {
                pagination.ItemsPerPage = value;
                await pagination.SetCurrentPageIndexAsync(0);
            }
            catch (Exception ex)
            {
                ToastService.ShowError($"Lỗi khi tải danh sách: {ex.Message}");
            }
        }
        public async Task RejectAsync(Guid id)
        {
            requestRestAPI.Endpoint = $"api/Product/{id}/Reject";
            ResultAPI result = await CallApi.Put(requestRestAPI, new object());
            if (result.Status == StatusCode.OK)
            {
                ToastService.ShowSuccess("Huỷ duyệt sản phẩm thành công");
                await ProductGrid.RefreshDataAsync();
            }
            else
            {
                ToastService.ShowError("Huỷ duyệt sản phẩm thất bại!  " + result.Message);
            }

        }
        public async Task ApproveAsync(Guid id)
        {
            requestRestAPI.Endpoint = $"api/Product/{id}/Approve";
            ResultAPI result = await CallApi.Put(requestRestAPI, new object());
            if (result.Status == StatusCode.OK)
            {
                ToastService.ShowSuccess("Duyệt sản phẩm thành công");
                await ProductGrid.RefreshDataAsync();

            }
            else
            {
                ToastService.ShowError("Duyệt sản phẩm thất bại!  " + result.Message);
            }

        }
        private async Task DeleteAsync(Guid id)
        {
            try
            {
                var dialog = await DialogService.ShowDialogAsync<ModalConfirm>(new DialogParameters());
                var resultDialog = await dialog.Result;
                if (resultDialog.Cancelled == false && resultDialog.Data is bool success && success)
                {
                    requestRestAPI.Endpoint = $"api/Product/{id}";
                    ResultAPI result = await CallApi.Delete(requestRestAPI);
                    if (result.Status == StatusCode.OK)
                    {
                        ToastService.ShowSuccess("Xóa sản phẩm thành công");
                        await ProductGrid.RefreshDataAsync();

                    }
                    else
                    {
                        ToastService.ShowError("Xóa sản phẩm thất bại: " + result.Message);

                    }
                }
            }
            catch (Exception ex)
            {
                ToastService.ShowError("Xóa thất bại: " + ex.Message);
            }

        }
        private async Task OpenModalAdd()
        {
            try
            {
                var parameters = new EditOrUpdateParameters
                {
                   IsEditMode=false,
                   OnRefresh = EventCallback.Factory.Create(this, RefreshDataAsync),
                };
                var dialog = await DialogService.ShowDialogAsync<Edit>(parameters, new DialogParameters
                {
                    Title = "Thêm sản phẩm",
                    PreventDismissOnOverlayClick = true,
                    PreventScroll = true,
                    Modal = true
                });
            }
            catch (Exception ex)
            {
                ToastService.ShowError($"Lỗi khi mở modal thêm sản phẩm: {ex.Message}");
            }
        }

        private async Task OpenModalUpdate(Guid id)
        {
            try
            {
                var parameters = new EditOrUpdateParameters
                {
                    Id = id,
                    IsEditMode = true,
                    OnRefresh = EventCallback.Factory.Create(this, RefreshDataAsync),
                };
                var dialog = await DialogService.ShowDialogAsync<Edit>(parameters, new DialogParameters
                {
                    Title = "Sửa sản phẩm",
                    PreventDismissOnOverlayClick = true,
                    PreventScroll = true,
                    Modal = true
                });
            }
            catch (Exception ex)
            {
                ToastService.ShowError($"Lỗi khi mở modal thêm sản phẩm: {ex.Message}");
            }
        }
        private async Task RefreshDataAsync()
        {
            await ProductGrid.RefreshDataAsync();
        }

    }
}
