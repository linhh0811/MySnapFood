using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using Service.SnapFood.Manage.Components.Share;
using Service.SnapFood.Manage.Dto;
using Service.SnapFood.Manage.Dto.Category;
using Service.SnapFood.Manage.Dto.ProductDto;
using Service.SnapFood.Manage.Infrastructure.Services;
using Service.SnapFood.Manage.Query;
using Service.SnapFood.Share.Interface.Extentions;
using Service.SnapFood.Share.Model.ServiceCustomHttpClient;
using Service.SnapFood.Share.Model.SQL;
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
        protected string SearchKeyword { get; set; } = string.Empty;
        private PaginationState pagination = new PaginationState { ItemsPerPage = 10 };
        private Dictionary<String, String> _SelectTrangThai = new Dictionary<string, string>();
        private string SelectedTrangThai = "-1";
        private string CategoryId = Guid.Empty.ToString();
        private List<CategoryDto> CategoryList { get; set; } = new List<CategoryDto>();
        private ColumnKeyGridSort<ProductDto> _roleNameSort = new ColumnKeyGridSort<ProductDto>("Quantity");


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
        private async ValueTask<GridItemsProviderResult<ProductDto>> LoadProduct(GridItemsProviderRequest<ProductDto> request)
        {
            try
            {
                if (!Enum.TryParse<ModerationStatus>(SelectedTrangThai, out var selectedTrangThai))
                {
                    selectedTrangThai = ModerationStatus.None; // Mặc định nếu không parse được
                }
                var baseQuery = new ProductQuery
                {
                    CategoryId = string.IsNullOrEmpty(CategoryId) ? Guid.Empty : Guid.Parse(CategoryId),
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
        #region duyệt, hủy duyệt, xóa
        public async Task RejectAsync(Guid id)
        {
            try
            {
                requestRestAPI.Endpoint = $"api/Product/{id}/CheckReject";
                ResultAPI resultCheck = await CallApi.Put(requestRestAPI, new object());
                if (resultCheck.Status == StatusCode.OK)
                {
                    var comboCount = Convert.ToInt32(resultCheck.Data?.ToString());
                    if (comboCount > 0)
                    {
                        var parameters = new RejectApproveParameters()
                        {
                            Title = "Hủy duyệt",
                            Content = $"Sản phẩm hiện tại nằm trong {comboCount} combo đang hoạt động.",
                            Content2 = "Nếu hủy duyệt sản phẩm này, các combo chứa sản phẩm cũng bị hủy duyệt."
                        };
                        var dialog = await DialogService.ShowDialogAsync<RejectConfirm>(parameters, new DialogParameters());
                        var resultDialog = await dialog.Result;
                        if (resultDialog.Cancelled == false && resultDialog.Data is bool success && success)
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
                    }
                    else
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
                requestRestAPI.Endpoint = $"api/Product/{id}/CheckApprove";
                ResultAPI resultCheck = await CallApi.Get<Dto.StringContent>(requestRestAPI);
                if (resultCheck.Status == StatusCode.OK)
                {
                    var resultData = resultCheck.Data as Dto.StringContent ?? new Dto.StringContent();
                    if (!string.IsNullOrEmpty(resultData.Content))
                    {
                        var parameters = new RejectApproveParameters()
                        {
                            Title = "Duyệt",
                            Content = $"{resultData.Content} của sản phẩm đang ngừng hoạt động.",
                            Content2 = $"Nếu duyệt sản phẩm này, {resultData.Content} của sản phẩm cũng sẽ được duyệt."
                        };
                        var dialog = await DialogService.ShowDialogAsync<RejectConfirm>(parameters, new DialogParameters());
                        var resultDialog = await dialog.Result;
                        if (resultDialog.Cancelled == false && resultDialog.Data is bool success && success)
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
                    }
                    else
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
                ToastService.ShowError($"Lỗi khi mở modal sửa sản phẩm: {ex.Message}");
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
                    Title = "Thông tin chi tiết sản phẩm",
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

        private async Task RefreshDataAsync()
        {
            await ProductGrid.RefreshDataAsync();
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
