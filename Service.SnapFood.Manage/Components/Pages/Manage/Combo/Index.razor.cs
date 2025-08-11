using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using Service.SnapFood.Manage.Components.Share;
using Service.SnapFood.Manage.Dto;
using Service.SnapFood.Manage.Dto.Category;
using Service.SnapFood.Manage.Dto.Combo;
using Service.SnapFood.Manage.Infrastructure.Services;
using Service.SnapFood.Manage.Query;
using Service.SnapFood.Share.Interface.Extentions;
using Service.SnapFood.Share.Model.Commons;
using Service.SnapFood.Share.Model.ServiceCustomHttpClient;
using Service.SnapFood.Share.Model.SQL;
using Service.SnapFood.Share.Query.Grid;
using System.Text.Json;

namespace Service.SnapFood.Manage.Components.Pages.Manage.Combo
{
    public partial class Index : ComponentBase
    {
        [CascadingParameter] public CurrentUser CurrentUser { get; set; } = new();
        [Inject] private ICallServiceRegistry CallApi { get; set; } = default!;
        [Inject] private IToastService ToastService { get; set; } = default!;
        [Inject] private IDialogService DialogService { get; set; } = default!;
        private ApiRequestModel requestRestAPI = new ApiRequestModel();
        protected FluentDataGrid<ComboDto> ComboGrid { get; set; } = default!;
        protected string SearchKeyword { get; set; } = string.Empty;
        private PaginationState pagination = new PaginationState { ItemsPerPage = 10 };
        private Dictionary<String, String> _SelectTrangThai = new Dictionary<string, string>();
        private string SelectedTrangThai = "-1";
        private string CategoryId = Guid.Empty.ToString();
        private List<CategoryDto> CategoryList { get; set; } = new List<CategoryDto>();
        private ColumnKeyGridSort<ComboDto> _roleNameSort = new ColumnKeyGridSort<ComboDto>("Quantity");

        protected override async Task OnInitializedAsync()
        {
            GetSelectTrangThai();
            await GetCategory();
        }
        private async Task GetCategory()
        {
            ApiRequestModel requestRestAPI = new ApiRequestModel();
            requestRestAPI.Endpoint = $"api/Category/";
            ResultAPI result = await CallApi.Get<List<CategoryDto>>(requestRestAPI);
            if (result.Status == StatusCode.OK)
            {
                CategoryList = result.Data as List<CategoryDto> ?? new List<CategoryDto>();
                CategoryDto cateogry = new CategoryDto()
                {
                    Id = Guid.Empty.ToString(),
                    CategoryName = "Chọn phân loại"
                };
                CategoryList.Insert(0, cateogry);
                CategoryId = CategoryList.First().Id ?? string.Empty;
            }
        }
        private async ValueTask<GridItemsProviderResult<ComboDto>> LoadCombo(GridItemsProviderRequest<ComboDto> request)
        {
            try
            {
                if (!Enum.TryParse<ModerationStatus>(SelectedTrangThai, out var selectedTrangThai))
                {
                    selectedTrangThai = ModerationStatus.None; // Mặc định nếu không parse được
                }
                var baseQuery = new ProductQuery
                {
                    ModerationStatus = selectedTrangThai,
                    CategoryId = string.IsNullOrEmpty(CategoryId) ? Guid.Empty : Guid.Parse(CategoryId),
                    SearchIn = new List<string> { "ComboName" },
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
                requestRestAPI.Endpoint = "api/Combo/GetPaged";

                ResultAPI result = await CallApi.Post<DataTableJson>(requestRestAPI, baseQuery);
                if (result.Status == StatusCode.OK && result.Data is DataTableJson dataTable)
                {
                    var items = JsonSerializer.Deserialize<List<ComboDto>>(dataTable.Data.GetRawText(),
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<ComboDto>(); ;



                    var totalRecords = dataTable.RecordsTotal ?? items.Count;
                    return GridItemsProviderResult.From(items, totalRecords);
                }
                StateHasChanged();
                return GridItemsProviderResult.From(new List<ComboDto>(), 0);
            }
            catch (Exception ex)
            {
                ToastService.ShowError($"Lỗi khi tải danh sách sản phẩm: {ex.Message}");
                return GridItemsProviderResult.From(new List<ComboDto>(), 0);
            }

        }
        #region oppen dialog
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
                    PreventDismissOnOverlayClick = true,
                    Title = null,
                    ShowDismiss = false,
                    PreventScroll = true,
                    Modal = true,
                    Width = "1400px",
                });
            }
            catch (Exception ex)
            {
                ToastService.ShowError($"Lỗi khi mở modal thêm combo: {ex.Message}");
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
                    PreventDismissOnOverlayClick = true,
                    Title = null,
                    ShowDismiss = false,
                    PreventScroll = true,
                    Modal = true,
                    Width = "1400px",
                });
            }
            catch (Exception ex)
            {
                ToastService.ShowError($"Lỗi khi mở modal thêm combo: {ex.Message}");
            }
        }
        #endregion

        #region RefresData
        private async Task RefreshDataAsync()
        {
            await ComboGrid.RefreshDataAsync();
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
        #endregion

        #region duyệt, hủy duyệt, xóa
        public async Task RejectAsync(Guid id)
        {
            requestRestAPI.Endpoint = $"api/Combo/{id}/Reject";
            ResultAPI result = await CallApi.Put(requestRestAPI, new object());
            if (result.Status == StatusCode.OK)
            {
                ToastService.ShowSuccess("Huỷ duyệt combo thành công");
                await ComboGrid.RefreshDataAsync();
            }
            else
            {
                ToastService.ShowError("Huỷ duyệt combo thất bại!  " + result.Message);
            }

        }
        public async Task ApproveAsync(Guid id)
        {
            try
            {
                requestRestAPI.Endpoint = $"api/Combo/{id}/CheckApprove";
                ResultAPI resultCheck = await CallApi.Get<Dto.StringContent>(requestRestAPI);
                if (resultCheck.Status == StatusCode.OK)
                {
                    var resultData = resultCheck.Data as Dto.StringContent ?? new Dto.StringContent();
                    if (!string.IsNullOrEmpty(resultData.Content))
                    {
                        var parameters = new RejectApproveParameters()
                        {
                            Title = "Duyệt",
                            Content = $"{resultData.Content} của combo đang ngừng hoạt động.",
                            Content2 = $"Nếu duyệt combo này những thành phần liên quan cũng sẽ được duyệt."
                        };
                        var dialog = await DialogService.ShowDialogAsync<RejectConfirm>(parameters, new DialogParameters());
                        var resultDialog = await dialog.Result;
                        if (resultDialog.Cancelled == false && resultDialog.Data is bool success && success)
                        {
                            requestRestAPI.Endpoint = $"api/Combo/{id}/Approve";
                            ResultAPI result = await CallApi.Put(requestRestAPI, new object());
                            if (result.Status == StatusCode.OK)
                            {
                                ToastService.ShowSuccess("Duyệt combo thành công");
                                await ComboGrid.RefreshDataAsync();

                            }
                            else
                            {
                                ToastService.ShowError("Duyệt combo thất bại!  " + result.Message);
                            }
                        }

                    }
                    else
                    {
                        requestRestAPI.Endpoint = $"api/Combo/{id}/Approve";
                        ResultAPI result = await CallApi.Put(requestRestAPI, new object());
                        if (result.Status == StatusCode.OK)
                        {
                            ToastService.ShowSuccess("Duyệt combo thành công");
                            await ComboGrid.RefreshDataAsync();

                        }
                        else
                        {
                            ToastService.ShowError("Duyệt combo thất bại!  " + result.Message);
                        }
                    }


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
                    requestRestAPI.Endpoint = $"api/Combo/{id}";
                    ResultAPI result = await CallApi.Delete(requestRestAPI);
                    if (result.Status == StatusCode.OK)
                    {
                        ToastService.ShowSuccess("Xóa combo thành công");
                        await ComboGrid.RefreshDataAsync();

                    }
                    else
                    {
                        ToastService.ShowError("Xóa combo thất bại: " + result.Message);

                    }
                }
            }
            catch (Exception ex)
            {
                ToastService.ShowError("Xóa thất bại: " + ex.Message);
            }

        }
        #endregion

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
                    Title = "Thông tin chi tiết combo",
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
