using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using Service.SnapFood.Manage.Components.Share;
using Service.SnapFood.Manage.Dto;
using Service.SnapFood.Manage.Dto.User;
using Service.SnapFood.Share.Interface.Extentions;
using Service.SnapFood.Share.Model.ServiceCustomHttpClient;
using Service.SnapFood.Share.Query;
using Service.SnapFood.Share.Query.Grid;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Service.SnapFood.Manage.Components.Pages.Manage.Role
{
    public partial class Edit : ComponentBase
    {
        [Inject] private ICallServiceRegistry CallApi { get; set; } = default!;
        [Inject] private IToastService ToastService { get; set; } = default!;
        [Inject] private IDialogService DialogService { get; set; } = default!;
        [CascadingParameter] public FluentDialog Dialog { get; set; } = default!;
        [Parameter] public EditOrUpdateParameters Content { get; set; } = new();
        private ApiRequestModel requestRestAPI = new ApiRequestModel();
        private PaginationState pagination = new PaginationState { ItemsPerPage = 10 };
        private string SearchKeyword { get; set; } = string.Empty;
        private FluentDataGrid<UserDto> UserGrid { get; set; } = default!;
        private bool isSaving = false;
        private string? ErrorMessage { get; set; }
        protected override async Task OnInitializedAsync()
        {
            if (UserGrid != null)
            {
                await UserGrid.RefreshDataAsync();
            }
            else
            {
                Console.WriteLine("UserGrid is null - Kiểm tra file Razor.");
            }
        }

        private async Task<bool> AddUserToRole(Guid userId)
        {
            ErrorMessage = null;
            try
            {
                isSaving = true;
                requestRestAPI.Endpoint = $"api/Role/{Content.Id}/AddUser";
                var response = await CallApi.Post<object>(requestRestAPI, userId);
                isSaving = false;

                if (response.Status == StatusCode.OK)
                {
                    ToastService.ShowSuccess("Thêm người dùng vào quyền thành công.");
                    await Content.OnRefresh.InvokeAsync();
                    if (UserGrid != null) await UserGrid.RefreshDataAsync();
                    return true;
                }
                else
                {
                    ErrorMessage = "Thêm người dùng thất bại: " + response.Message;
                    ToastService.ShowError(ErrorMessage);
                    return false;
                }
            }
            catch (Exception ex)
            {
                isSaving = false;
                ErrorMessage = "Thêm người dùng thất bại: " + ex.Message;
                ToastService.ShowError(ErrorMessage);
                return false;
            }
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
                requestRestAPI.Endpoint = $"api/Role/{Content.Id}/GetAllUsersPaged";
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

        private async Task RefreshData(int value)
        {
            pagination.ItemsPerPage = value;
            await pagination.SetCurrentPageIndexAsync(0);
            await RefreshDataAsync();
        }

        private async Task RefreshDataAsync()
        {
            if (UserGrid != null) await UserGrid.RefreshDataAsync();
        }
    }
}