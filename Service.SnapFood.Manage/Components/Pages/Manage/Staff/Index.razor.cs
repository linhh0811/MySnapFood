using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using Microsoft.JSInterop;
using Service.SnapFood.Manage.Components.Share;
using Service.SnapFood.Manage.Dto;
using Service.SnapFood.Manage.Dto.StaffDto;
using Service.SnapFood.Share.Interface.Extentions;
using Service.SnapFood.Share.Model.ServiceCustomHttpClient;
using Service.SnapFood.Share.Query;
using Service.SnapFood.Share.Query.Grid;
using System.Text.Json;
namespace Service.SnapFood.Manage.Components.Pages.Manage.Staff
{
    public partial class Index : ComponentBase
    {
        [Inject] private ICallServiceRegistry CallApi { get; set; } = default!;
        [Inject] private IToastService ToastService { get; set; } = default!;
        [Inject] private IDialogService DialogService { get; set; } = default!;
        private ApiRequestModel requestRestAPI = new ApiRequestModel();
        protected FluentDataGrid<StaffDto> StaffGrid { get; set; } = default!;
        protected string SearchKeyword { get; set; } = string.Empty;
        private PaginationState pagination = new PaginationState { ItemsPerPage = 10 };
        private async ValueTask<GridItemsProviderResult<StaffDto>> LoadStaff(GridItemsProviderRequest<StaffDto> request)
        {
            try
            {
                var baseQuery = new BaseQuery
                {
                    SearchIn = new List<string> { "FullName", "Email", "Numberphone" },
                    Keyword = SearchKeyword,
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
                requestRestAPI.Endpoint = "api/Staff/GetPaged";

                ResultAPI result = await CallApi.Post<DataTableJson>(requestRestAPI, baseQuery);
                if (result.Status == StatusCode.OK && result.Data is DataTableJson dataTable)
                {
                    var items = JsonSerializer.Deserialize<List<StaffDto>>(dataTable.Data.GetRawText(),
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<StaffDto>(); ;



                    var totalRecords = dataTable.RecordsTotal ?? items.Count;
                    return GridItemsProviderResult.From(items, totalRecords);
                }
                StateHasChanged();
                return GridItemsProviderResult.From(new List<StaffDto>(), 0);
            }
            catch (Exception ex)
            {
                ToastService.ShowError($"Lỗi khi tải danh sách nhân viên: {ex.Message}");
                return GridItemsProviderResult.From(new List<StaffDto>(), 0);
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

        #region duyệt, hủy duyệt, xóa
        public async Task RejectAsync(string id)
        {
            requestRestAPI.Endpoint = $"api/Staff/{id}/Reject";
            ResultAPI result = await CallApi.Put(requestRestAPI, new object());
            if (result.Status == StatusCode.OK)
            {
                ToastService.ShowSuccess("Huỷ duyệt nhân viên thành công");
                await StaffGrid.RefreshDataAsync();
            }
            else
            {
                ToastService.ShowError("Huỷ duyệt nhân viên thất bại!  " + result.Message);
            }

        }

        public async Task ApproveAsync(string id)
        {
            requestRestAPI.Endpoint = $"api/Staff/{id}/Approve";
            ResultAPI result = await CallApi.Put(requestRestAPI, new object());
            if (result.Status == StatusCode.OK)
            {
                ToastService.ShowSuccess("Duyệt nhân viên thành công");
                await StaffGrid.RefreshDataAsync();

            }
            else
            {
                ToastService.ShowError("Duyệt nhân viên thất bại!  " + result.Message);
            }

        }

        private async Task DeleteAsync(string id)
        {
            try
            {
                var dialog = await DialogService.ShowDialogAsync<ModalConfirm>(new DialogParameters());
                var resultDialog = await dialog.Result;
                if (resultDialog.Cancelled == false && resultDialog.Data is bool success && success)
                {
                    requestRestAPI.Endpoint = $"api/Staff/{id}";
                    ResultAPI result = await CallApi.Delete(requestRestAPI);
                    if (result.Status == StatusCode.OK)
                    {
                        ToastService.ShowSuccess("Xóa nhân viên thành công");
                        await StaffGrid.RefreshDataAsync();

                    }
                    else
                    {
                        ToastService.ShowError("Xóa nhân viên thất bại: " + result.Message);

                    }
                }
            }
            catch (Exception ex)
            {
                ToastService.ShowError("Xóa thất bại: " + ex.Message);
            }

        }

        #endregion
        private async Task OpenModalAdd()
        {
            try
            {
                var parameters = new EditOrUpdateParameters
                {
                    IsEditMode = false,
                    OnRefresh = EventCallback.Factory.Create(this, RefreshDataAsync),
                };
                var dialog = await DialogService.ShowDialogAsync<Edit>(parameters, new DialogParameters
                {
                    Title = "Thêm nhân viên",
                    PreventDismissOnOverlayClick = true,
                    PreventScroll = true,
                    Modal = true
                });
            }
            catch (Exception ex)
            {
                ToastService.ShowError($"Lỗi khi mở modal thêm nhân viên: {ex.Message}");
            }
        }

        private async Task OpenModalUpdate(string id)
        {
            try
            {
                var parameters = new EditOrUpdateParameters
                {
                    Id =Guid.Parse(id),
                    IsEditMode = true,
                    OnRefresh = EventCallback.Factory.Create(this, RefreshDataAsync),
                };
                var dialog = await DialogService.ShowDialogAsync<Edit>(parameters, new DialogParameters
                {
                    Title = "Sửa nhân viên",
                    PreventDismissOnOverlayClick = true,
                    PreventScroll = true,
                    Modal = true
                });
            }
            catch (Exception ex)
            {
                ToastService.ShowError($"Lỗi khi mở modal thêm nhân viên: {ex.Message}");
            }
        }


        private async Task RefreshDataAsync()
        {
            await StaffGrid.RefreshDataAsync();
        }
    }
}

