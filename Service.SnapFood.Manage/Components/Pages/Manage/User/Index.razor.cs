using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using Service.SnapFood.Manage.Components.Share;
using Service.SnapFood.Manage.Dto;
using Service.SnapFood.Manage.Dto.User;
using Service.SnapFood.Share.Interface.Extentions;
using Service.SnapFood.Share.Model.ServiceCustomHttpClient;
using Service.SnapFood.Share.Query;
using Service.SnapFood.Share.Query.Grid;
using System.Text.Json;

namespace Service.SnapFood.Manage.Components.Pages.Manage.User
{
    public partial class Index : ComponentBase
    {
        [Inject] private ICallServiceRegistry CallApi { get; set; } = default!;
        [Inject] private IToastService ToastService { get; set; } = default!;
        [Inject] private IDialogService DialogService { get; set; } = default!;
        private ApiRequestModel requestRestAPI = new ApiRequestModel();
        protected FluentDataGrid<UserDto> UserGrid { get; set; } = default!;
        protected string SearchKeyword { get; set; } = string.Empty;
        private PaginationState pagination = new PaginationState { ItemsPerPage = 10 };
        protected override async Task OnInitializedAsync()
        {
            // Tải dữ liệu ban đầu khi component khởi tạo
            await RefreshDataAsync();
        }
        private async ValueTask<GridItemsProviderResult<UserDto>> LoadUsers(GridItemsProviderRequest<UserDto> request)
        {
            try
            {
                var baseQuery = new BaseQuery
                {
                    SearchIn = new List<string> { "FullName", "Email" },
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
                requestRestAPI.Endpoint = "api/User/GetPaged";

                ResultAPI result = await CallApi.Post<DataTableJson>(requestRestAPI, baseQuery);
                if (result.Status == StatusCode.OK && result.Data is DataTableJson dataTable)
                {
                    var items = JsonSerializer.Deserialize<List<UserDto>>(dataTable.Data.GetRawText(),
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<UserDto>();

                    var totalRecords = dataTable.RecordsTotal ?? items.Count;
                    return GridItemsProviderResult.From(items, totalRecords);
                }
                return GridItemsProviderResult.From(new List<UserDto>(), 0);
            }
            catch (Exception ex)
            {
                ToastService.ShowError($"Lỗi khi tải danh sách người dùng: {ex.Message}");
                return GridItemsProviderResult.From(new List<UserDto>(), 0);
            }
        }

        private async Task RefreshDataAsync()
        {
            try
            {
                if (UserGrid != null)
                {
                    await UserGrid.RefreshDataAsync();
                }
            }
            catch (Exception ex)
            {
                ToastService.ShowError($"Lỗi khi làm mới dữ liệu: {ex.Message}");
            }
        }

        private async Task RefresData(int value)
        {
            try
            {
                pagination.ItemsPerPage = value;
                await pagination.SetCurrentPageIndexAsync(0);
                await RefreshDataAsync();
            }
            catch (Exception ex)
            {
                ToastService.ShowError($"Lỗi khi thay đổi kích thước trang: {ex.Message}");
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
                    Title = "Thông tin chi tiết khách hàng",
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
}