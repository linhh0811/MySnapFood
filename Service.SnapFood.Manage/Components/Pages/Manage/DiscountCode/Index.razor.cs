using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using Service.SnapFood.Manage.Components.Share;
using Service.SnapFood.Manage.Dto;
using Service.SnapFood.Manage.Dto.DiscountCode;
using Service.SnapFood.Manage.Infrastructure.Services;
using Service.SnapFood.Manage.Query;
using Service.SnapFood.Share.Interface.Extentions;
using Service.SnapFood.Share.Model.ServiceCustomHttpClient;
using Service.SnapFood.Share.Model.SQL;
using Service.SnapFood.Share.Query.Grid;
using System.Text.Json;

namespace Service.SnapFood.Manage.Components.Pages.Manage.DiscountCode
{
    public partial class Index : ComponentBase
    {
        [Inject] private ICallServiceRegistry CallApi { get; set; } = default!;
        [Inject] private IToastService ToastService { get; set; } = default!;
        [Inject] private IDialogService DialogService { get; set; } = default!;

        private ApiRequestModel requestRestAPI = new ApiRequestModel();
        protected FluentDataGrid<DiscountCodeDto> DiscountCodeGrid { get; set; } = default!;
        protected string SearchKeyword { get; set; } = string.Empty;
        private PaginationState pagination = new PaginationState { ItemsPerPage = 10 };
        private Dictionary<string, string> _SelectTrangThai = new();
        private string SelectedTrangThai = "-1";

        protected override async Task OnInitializedAsync()
        {
            GetSelectTrangThai();
        }

        private async ValueTask<GridItemsProviderResult<DiscountCodeDto>> LoadDiscountCodes(GridItemsProviderRequest<DiscountCodeDto> request)
        {
            try
            {
                if (!Enum.TryParse<ModerationStatus>(SelectedTrangThai, out var selectedTrangThai))
                    selectedTrangThai = ModerationStatus.None;

                var baseQuery = new DiscountCodeQuery
                {
                    ModerationStatus = selectedTrangThai,
                    Keyword = SearchKeyword,
                    gridRequest = new GridRequest
                    {
                        page = (request.StartIndex / pagination.ItemsPerPage) + 1,
                        pageSize = pagination.ItemsPerPage,
                        skip = request.StartIndex,
                        take = pagination.ItemsPerPage,
                        sort = request.GetSortByProperties().Select(s => new Sort
                        {
                            field = s.PropertyName,
                            dir = s.Direction == SortDirection.Ascending ? "asc" : "desc"
                        }).ToList()
                    }
                };

                requestRestAPI.Endpoint = "api/DiscountCode/GetPaged";
                ResultAPI result = await CallApi.Post<DataTableJson>(requestRestAPI, baseQuery);

                if (result.Status == StatusCode.OK && result.Data is DataTableJson dataTable)
                {
                    var items = JsonSerializer.Deserialize<List<DiscountCodeDto>>(dataTable.Data.GetRawText(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new();
                    var totalRecords = dataTable.RecordsTotal ?? items.Count;
                    return GridItemsProviderResult.From(items, totalRecords);
                }
                return GridItemsProviderResult.From(new List<DiscountCodeDto>(), 0);
            }
            catch (Exception ex)
            {
                ToastService.ShowError($"Lỗi khi tải danh sách mã giảm giá: {ex.Message}");
                return GridItemsProviderResult.From(new List<DiscountCodeDto>(), 0);
            }
        }

        public async Task RejectAsync(Guid id)
        {
            try
            {
                requestRestAPI.Endpoint = $"api/DiscountCode/{id}/Reject";
                ResultAPI result = await CallApi.Put(requestRestAPI, new object());
                if (result.Status == StatusCode.OK)
                {
                    ToastService.ShowSuccess("Huỷ duyệt mã giảm giá thành công");
                    await DiscountCodeGrid.RefreshDataAsync();
                }
                else
                {
                    ToastService.ShowError("Huỷ duyệt thất bại: " + result.Message);
                }
            }
            catch (Exception ex)
            {
                ToastService.ShowError("Huỷ duyệt thất bại: " + ex.Message);
            }
        }

        public async Task ApproveAsync(Guid id)
        {
            try
            {
                requestRestAPI.Endpoint = $"api/DiscountCode/{id}/Approve";
                ResultAPI result = await CallApi.Put(requestRestAPI, new object());
                if (result.Status == StatusCode.OK)
                {
                    ToastService.ShowSuccess("Duyệt mã giảm giá thành công");
                    await DiscountCodeGrid.RefreshDataAsync();
                }
                else
                {
                    ToastService.ShowError("Duyệt thất bại: " + result.Message);
                }
            }
            catch (Exception ex)
            {
                ToastService.ShowError("Duyệt thất bại: " + ex.Message);
            }
        }

        private async Task DeleteAsync(Guid id)
        {
            try
            {
                var dialog = await DialogService.ShowDialogAsync<ModalConfirm>(new DialogParameters());
                var resultDialog = await dialog.Result;
                if (!resultDialog.Cancelled && resultDialog.Data is bool success && success)
                {
                    requestRestAPI.Endpoint = $"api/DiscountCode/{id}";
                    ResultAPI result = await CallApi.Delete(requestRestAPI);
                    if (result.Status == StatusCode.OK)
                    {
                        ToastService.ShowSuccess("Xóa mã giảm giá thành công");
                        await DiscountCodeGrid.RefreshDataAsync();
                    }
                    else
                    {
                        ToastService.ShowError("Xóa thất bại: " + result.Message);
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
                    IsEditMode = false,
                    OnRefresh = EventCallback.Factory.Create(this, RefreshDataAsync),
                };
                await DialogService.ShowDialogAsync<Edit>(parameters, new DialogParameters
                {
                    Title = "Thêm mã giảm giá",
                    PreventDismissOnOverlayClick = true,
                    PreventScroll = true,
                    Modal = true,
                    Width = "1000px"
                });
            }
            catch (Exception ex)
            {
                ToastService.ShowError($"Lỗi khi mở modal thêm: {ex.Message}");
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

                //var dialogOptions = new DialogParameters
                var dialog = await DialogService.ShowDialogAsync<Edit>(parameters, new DialogParameters
                {
                    Title = "Sửa mã giảm giá",
                    PreventDismissOnOverlayClick = true,
                    PreventScroll = true,
                    Modal = true,
                    Width = "1000px"
                });

            }
            catch (Exception ex)
            {
                ToastService.ShowError($"Lỗi khi mở modal sửa: {ex.Message}");
            }
        }


        private async Task OpenDetailsModal(Guid Id)
        {
            try
            {
                var parameters = new EditOrUpdateParameters
                {
                    Id = Id,
                    RequestApi = requestRestAPI,
                };
                await DialogService.ShowDialogAsync<View>(parameters, new DialogParameters
                {
                    Title = "Thông tin chi tiết mã giảm giá",
                    PreventDismissOnOverlayClick = true,
                    PreventScroll = true,
                    Modal = true,
                    Width = "700px",
                    TrapFocus = false
                });
            }
            catch (Exception ex)
            {
                ToastService.ShowError($"Lỗi khi mở modal chi tiết: {ex.Message}");
            }
        }



        private async Task RefreshDataAsync()
        {
            await DiscountCodeGrid.RefreshDataAsync();
        }

        private void GetSelectTrangThai()
        {
            _SelectTrangThai = Enum.GetValues(typeof(ModerationStatus))
                .Cast<ModerationStatus>()
                .OrderBy(e => e == ModerationStatus.None ? 0 : 1)
                .ThenBy(e => e)
                .ToDictionary(
                    e => e.ToString(),
                    e => e.GetDescription()
                );
        }
    }
}
