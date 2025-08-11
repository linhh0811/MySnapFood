
using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using Service.SnapFood.Manage.Components.Share;
using Service.SnapFood.Manage.Dto;
using Service.SnapFood.Manage.Dto.Promotion;
using Service.SnapFood.Manage.Infrastructure.Services;
using Service.SnapFood.Manage.Query;
using Service.SnapFood.Share.Interface.Extentions;
using Service.SnapFood.Share.Model.Commons;
using Service.SnapFood.Share.Model.ServiceCustomHttpClient;
using Service.SnapFood.Share.Model.SQL;
using Service.SnapFood.Share.Query.Grid;
using System.Text.Json;

namespace Service.SnapFood.Manage.Components.Pages.Manage.Promotion
{
    public partial class Index : ComponentBase
    {
        [CascadingParameter] public CurrentUser CurrentUser { get; set; } = new();
        [Inject] private ICallServiceRegistry CallApi { get; set; } = default!;
        [Inject] private IToastService ToastService { get; set; } = default!;
        [Inject] private IDialogService DialogService { get; set; } = default!;
        private ApiRequestModel requestRestAPI = new ApiRequestModel();
        protected FluentDataGrid<PromotionDto> PromotionGrid { get; set; } = default!;
        protected string SearchKeyword { get; set; } = string.Empty;
        private PaginationState pagination = new PaginationState { ItemsPerPage = 10 };
        private Dictionary<String, String> _SelectTrangThai = new Dictionary<string, string>();
        private string SelectedTrangThai = "-1";
        private Dictionary<String, String> _SelectLoai = new Dictionary<string, string>();

        private string SelectedLoai = "-1";

        protected override void OnInitialized()
        {
            GetSelectTrangThai();
            GetSelectLoai();
        }

        private async ValueTask<GridItemsProviderResult<PromotionDto>> LoadPromotion(GridItemsProviderRequest<PromotionDto> request)
        {
            try
            {
                if (!Enum.TryParse<ModerationStatus>(SelectedTrangThai, out var selectedTrangThai))
                {
                    selectedTrangThai = ModerationStatus.None; // Mặc định nếu không parse được
                }
                if (!Enum.TryParse<PromotionType>(SelectedLoai, out var selectedLoai))
                {
                    selectedLoai = PromotionType.None; // Mặc định nếu không parse được
                }
                var baseQuery = new PromotionQuery
                {
                    PromotionType = selectedLoai,
                    ModerationStatus = selectedTrangThai,
                    SearchIn = new List<string> { "ProductName" },
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
                requestRestAPI.Endpoint = "api/Promotion/GetPaged";

                ResultAPI result = await CallApi.Post<Dto.DataTableJson>(requestRestAPI, baseQuery);
                if (result.Status == StatusCode.OK && result.Data is Dto.DataTableJson dataTable)
                {
                    var items = JsonSerializer.Deserialize<List<PromotionDto>>(dataTable.Data.GetRawText(),
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<PromotionDto>(); ;



                    var totalRecords = dataTable.RecordsTotal ?? items.Count;
                    return GridItemsProviderResult.From(items, totalRecords);
                }
                StateHasChanged();
                return GridItemsProviderResult.From(new List<PromotionDto>(), 0);
            }
            catch (Exception ex)
            {
                ToastService.ShowError($"Lỗi khi tải danh sách khuyến mại: {ex.Message}");
                return GridItemsProviderResult.From(new List<PromotionDto>(), 0);
            }

        }
        #region duyệt, hủy duyệt, xóa
        public async Task RejectAsync(Guid id)
        {
            try
            {
                requestRestAPI.Endpoint = $"api/Promotion/{id}/Reject";
                ResultAPI result = await CallApi.Put(requestRestAPI, new object());
                if (result.Status == StatusCode.OK)
                {
                    ToastService.ShowSuccess("Huỷ duyệt khuyến mại thành công");
                    await PromotionGrid.RefreshDataAsync();
                }
                else
                {
                    ToastService.ShowError("Huỷ duyệt khuyến mại thất bại!  " + result.Message);
                }

            }
            catch (Exception ex)
            {
                ToastService.ShowError("Hủy duyệt thất bại: " + ex.Message);
            }


        }
        public async Task ApproveAsync(Guid id)
        {
            try
            {

                requestRestAPI.Endpoint = $"api/Promotion/{id}/Approve";
                ResultAPI result = await CallApi.Put(requestRestAPI, new object());
                if (result.Status == StatusCode.OK)
                {
                    ToastService.ShowSuccess("Duyệt khuyến mại thành công");
                    await PromotionGrid.RefreshDataAsync();

                }
                else
                {
                    ToastService.ShowError("Duyệt khuyến mại thất bại!  " + result.Message);
                }




            }
            catch (Exception ex)
            {
                ToastService.ShowError("Hủy duyệt thất bại: " + ex.Message);
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
                    requestRestAPI.Endpoint = $"api/Promotion/{id}";
                    ResultAPI result = await CallApi.Delete(requestRestAPI);
                    if (result.Status == StatusCode.OK)
                    {
                        ToastService.ShowSuccess("Xóa sản phẩm thành công");
                        await PromotionGrid.RefreshDataAsync();

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
                    Title = "Thêm khuyến mại",
                    PreventDismissOnOverlayClick = true,
                    PreventScroll = true,
                    Modal = true,
                    Width = "1250px"

                });
            }
            catch (Exception ex)
            {
                ToastService.ShowError($"Lỗi khi mở modal thêm khuyến mại: {ex.Message}");
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
                    Title = "Sửa khuyến mại",
                    PreventDismissOnOverlayClick = true,
                    PreventScroll = true,
                    Modal = true,
                    Width = "1250px"

                });
            }
            catch (Exception ex)
            {
                ToastService.ShowError($"Lỗi khi mở modal sửa khuyến mại: {ex.Message}");
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
                    Title = "Thông tin chi tiết khuyến mãi",
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
        private async Task RefreshDataAsync()
        {
            await PromotionGrid.RefreshDataAsync();
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

        private void GetSelectLoai()
        {
            _SelectLoai = Enum.GetValues(typeof(PromotionType))
                .Cast<PromotionType>()
                .OrderBy(e => e == PromotionType.None ? 0 : 1)
                .ThenBy(e => e)
                .ToDictionary(
                    e => e.ToString(),
                    e => e.GetDescription()
                );
        }
    }
}
