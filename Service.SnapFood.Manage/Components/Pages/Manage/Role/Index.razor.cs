using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using Service.SnapFood.Manage.Components.Share;
using Service.SnapFood.Manage.Dto;
using Service.SnapFood.Manage.Dto.Role;
using Service.SnapFood.Manage.Dto.TreeView;
using Service.SnapFood.Manage.Dto.User;
using Service.SnapFood.Share.Interface.Extentions;
using Service.SnapFood.Share.Model.ServiceCustomHttpClient;
using Service.SnapFood.Share.Query;
using Service.SnapFood.Share.Query.Grid;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Threading.Tasks;

namespace Service.SnapFood.Manage.Components.Pages.Manage.Role
{
    public partial class Index : ComponentBase
    {
        [Inject] private ICallServiceRegistry CallApi { get; set; } = default!;
        [Inject] private IDialogService DialogService { get; set; } = default!;
        [Inject] private IToastService ToastService { get; set; } = default!;

        private ApiRequestModel requestRestAPI = new ApiRequestModel();
        private ITreeViewItem? selectedRole;
        private IEnumerable<ITreeViewItem> Roles = new List<ITreeViewItem>();
        private List<RoleDto> roleList = new List<RoleDto>();
        private PaginationState pagination = new PaginationState { ItemsPerPage = 10 };
        private string KeySearchUser { get; set; } = string.Empty;
        private FluentDataGrid<UserDto> UserGrid { get; set; } = default!;
        private bool isLoading = false;

        protected override async Task OnInitializedAsync()
        {
            isLoading = true;
            await GetRoles();
            if (Roles.Any())
            {
                selectedRole = Roles.First(); // Chọn quyền đầu tiên mặc định
                await LoadUsersForRole(selectedRole.Id);
            }
            isLoading = false;
        }

        private async Task GetRoles()
        {
            requestRestAPI.Endpoint = "api/Role";
            ResultAPI result = await CallApi.Get<List<RoleDto>>(requestRestAPI);
            if (result.Status == StatusCode.OK)
            {
                roleList = result.Data as List<RoleDto> ?? new List<RoleDto>();
                Roles = roleList.Select(r => new TreeViewItemDto { Id = r.Id.ToString(), Text = r.RoleName }).ToList();
            }
            else
            {
                ToastService.ShowError("Không thể tải danh sách quyền: " + result.Message);
            }
        }

        private void SelectRole(ITreeViewItem role)
        {
            selectedRole = role;
            _ = LoadUsersForRole(role.Id);
        }

        private async Task LoadUsersForRole(string roleId)
        {
            await UserGrid.RefreshDataAsync();
        }

        private async ValueTask<GridItemsProviderResult<UserDto>> LoadUsers(GridItemsProviderRequest<UserDto> request)
        {
            if (selectedRole == null) return GridItemsProviderResult.From(new List<UserDto>(), 0);

            try
            {
                var baseQuery = new BaseQuery
                {
                    SearchIn = new List<string> { "FullName", "Email" },
                    Keyword = KeySearchUser,
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
                requestRestAPI.Endpoint = $"api/Role/{selectedRole.Id}/Users/GetPaged";
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

        private async Task OpenModalAddUserToRole()
        {
            if (selectedRole == null)
            {
                ToastService.ShowWarning("Vui lòng chọn một quyền trước khi thêm người dùng.");
                return;
            }

            try
            {
                var parameters = new EditOrUpdateParameters
                {
                    Id = Guid.Parse(selectedRole.Id),
                    IsEditMode = false,
                    OnRefresh = EventCallback.Factory.Create(this, RefreshDataAsync)
                };
                var dialog = await DialogService.ShowDialogAsync<Edit>(parameters, new DialogParameters
                {
                    Title = "Thêm người dùng vào quyền",
                    PreventDismissOnOverlayClick = true,
                    PreventScroll = true,
                    Modal = true,
                    Width ="800px",
                    ShowDismiss=false,
                    ShowTitle=false
                });
            }
            catch (Exception ex)
            {
                ToastService.ShowError($"Lỗi khi mở modal thêm người dùng: {ex.Message}");
            }
        }

        private async Task DeleteUserFromRole(Guid userId)
        {
            if (selectedRole == null)
            {
                ToastService.ShowWarning("Vui lòng chọn một quyền trước khi xóa người dùng.");
                return;
            }

            var confirmDialog = await DialogService.ShowDialogAsync<ModalConfirm>(new DialogParameters<RequestModalConfirm>
            {
                Content = new RequestModalConfirm
                {
                    Title = "Xác nhận xóa",
                    Content = "Bạn có chắc chắn muốn xóa người dùng này khỏi quyền?",
                    Message = "Hành động này không thể hoàn tác."
                }
            });
            var result = await confirmDialog.Result;
            if (!result.Cancelled && result.Data is bool success && success)
            {
                requestRestAPI.Endpoint = $"api/Role/{selectedRole.Id}/RemoveUser";
                var response = await CallApi.Post<object>(requestRestAPI, userId);
                if (response.Status == StatusCode.OK)
                {
                    ToastService.ShowSuccess("Xóa người dùng khỏi quyền thành công.");
                    await LoadUsersForRole(selectedRole.Id);
                }
                else
                {
                    ToastService.ShowError("Xóa người dùng thất bại: " + response.Message);
                }
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
            await UserGrid.RefreshDataAsync();
        }
    }
}