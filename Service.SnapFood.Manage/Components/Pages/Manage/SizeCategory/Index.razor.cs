using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components.Extensions;
using Service.SnapFood.Manage.Components.Share;
using Service.SnapFood.Manage.Dto;
using Service.SnapFood.Manage.Dto.SizeDto;
using Service.SnapFood.Manage.Dto.TreeView;
using Service.SnapFood.Share.Interface.Extentions;
using Service.SnapFood.Share.Model.ServiceCustomHttpClient;
using Service.SnapFood.Share.Query.Grid;
using Service.SnapFood.Share.Query;
using System.Reflection;
using System.Text.Json;
using Service.SnapFood.Manage.Dto.ProductDto;
using Service.SnapFood.Manage.Dto.Category;

namespace Service.SnapFood.Manage.Components.Pages.Manage.SizeCategory
{
    public partial class Index : ComponentBase
    {
        [Inject] private ICallServiceRegistry CallApi { get; set; } = default!;
        [Inject] private IDialogService DialogService { get; set; } = default!;
        [Inject] private IToastService ToastService { get; set; } = default!;
        private ApiRequestModel requestRestAPI = new ApiRequestModel() { };
        private ITreeViewItem? selectedItem;
        private IEnumerable<ITreeViewItem> Items = new List<ITreeViewItem>();
        private List<SizeTreeDto>? sizeTree;
        private List<SizeTreeDto> sizeParent = new List<SizeTreeDto>();
        private bool isLoading = true;
        private PaginationState pagination = new PaginationState { ItemsPerPage = 10 };
        private string KeySearchCategory { get; set; } = string.Empty;

        protected FluentDataGrid<CategoryDto> CategoryGrid { get; set; } = default!;

        protected override async Task OnInitializedAsync()
        {
            isLoading = true;
            await GetSizeTree();
            isLoading = false;
        }
        #region size
        public async Task GetSizeTree()
        {
            requestRestAPI.Endpoint = $"api/Size/Tree";
            ResultAPI result = await CallApi.Get<List<SizeTreeDto>>(requestRestAPI);
            if (result.Status == StatusCode.OK)
            {
                sizeTree = result.Data as List<SizeTreeDto>??new List<SizeTreeDto>();
                sizeParent = sizeTree.Where(x => x.ParentId == null).ToList();
                Items = ConvertGroupTypeItems(sizeTree);
            }

        }
        
        private string GetTextStyle(int status)
        {
            return status switch
            {
                0 => "",
                1 => "text-decoration: line-through; font-style: italic;",
                2 => "text-decoration: line-through; font-style: italic;",
                _ => ""
            };
        }

        private void SelectTreeItem(ITreeViewItem item)
        {
            selectedItem = item;
        }

        private IEnumerable<ITreeViewItem> ConvertGroupTypeItems(List<SizeTreeDto>? list)
        {
            if (list is null)
            {
                return Enumerable.Empty<ITreeViewItem>();
            }

            var result = list.Select(x => new TreeViewItemDto
            {
                Id=x.Id,
                ModerationStatus=Convert.ToInt32(x.ModerationStatus),
                Text = x.SizeName,
                Items = x.Children != null && x.Children.Any() ? ConvertToTreeItems(x.Children).ToList() : null,
                Expanded = true
            }).ToList();

            return result;
        }
        private IEnumerable<ITreeViewItem> ConvertToTreeItems(List<SizeTreeDto>? list)
        {
            if (list is null)
            {
                return Enumerable.Empty<ITreeViewItem>();
            }

            var result = list.Select(x =>
            {
                var item = new TreeViewItemDto
                {
                    Text = x.SizeName+" "+$"(+ {x.AdditionalPrice.ToString("N0")} đ)",
                    ModerationStatus =Convert.ToInt32(x.ModerationStatus),
                    Id = x.Id.ToString(),
                    Items = x.Children != null && x.Children.Any() ? ConvertToTreeItems(x.Children).ToList() : null,
                    Expanded = true,
                };

                return item;
            }).ToList();

            return result;
        }
        public async Task RejectSizeAsync(string id)
        {
            requestRestAPI.Endpoint = $"api/Size/{id}/Reject";
            ResultAPI result = await CallApi.Put(requestRestAPI, new object());
            if (result.Status == StatusCode.OK)
            {
                ToastService.ShowSuccess("Huỷ duyệt size thành công");
                await GetSizeTree();
            }
            else
            {
                ToastService.ShowError("Huỷ duyệt size thất bại!  " + result.Message);
            }

        }
        public async Task ApproveSizeAsync(string id)
        {
            requestRestAPI.Endpoint = $"api/Size/{id}/Approve";
            ResultAPI result = await CallApi.Put(requestRestAPI, new object());
            if (result.Status == StatusCode.OK)
            {
                ToastService.ShowSuccess("Duyệt size thành công");
                await GetSizeTree();

            }
            else
            {
                ToastService.ShowError("Duyệt size thất bại!  " + result.Message);
            }

        }
        private async Task DeleteSizeAsync(string id)
        {
            try
            {
                var dialog = await DialogService.ShowDialogAsync<ModalConfirm>(new DialogParameters());
                var resultDialog = await dialog.Result;
                if (resultDialog.Cancelled == false && resultDialog.Data is bool success && success)
                {
                    requestRestAPI.Endpoint = $"api/Size/{id}";
                    ResultAPI result = await CallApi.Delete(requestRestAPI);
                    if (result.Status == StatusCode.OK)
                    {
                        ToastService.ShowSuccess("Xóa size thành công");
                        await GetSizeTree();
                    }
                    else
                    {
                        ToastService.ShowError("Xóa size thất bại: " + result.Message);

                    }
                }
            }
            catch (Exception ex)
            {
                ToastService.ShowError("Xóa thất bại: " + ex.Message);
            }

        }


        private async Task OpenModalAddSize()
        {
            try
            {
                var parameters = new EditOrUpdateParameters
                {
                    IsEditMode = false,
                    OnRefresh = EventCallback.Factory.Create(this, GetSizeTree),
                    Data=sizeParent,

                };
                var dialog = await DialogService.ShowDialogAsync<EditSize>(parameters, new DialogParameters
                {
                    Title = "Thêm size",
                    PreventDismissOnOverlayClick = true,
                    PreventScroll = true,
                    Modal = true
                });
            }
            catch (Exception ex)
            {
                ToastService.ShowError($"Lỗi khi mở modal thêm size: {ex.Message}");
            }
        }
        private async Task OpenModalUpdateSize(string id)
        {
            try
            {
                var parameters = new EditOrUpdateParameters
                {
                    Id= string.IsNullOrEmpty(id) ? Guid.Empty : Guid.Parse(id),
                    IsEditMode = true,
                    OnRefresh = EventCallback.Factory.Create(this, GetSizeTree),
                    Data = sizeParent,

                };
                var dialog = await DialogService.ShowDialogAsync<EditSize>(parameters, new DialogParameters
                {
                    Title = "Sửa size",
                    PreventDismissOnOverlayClick = true,
                    PreventScroll = true,
                    Modal = true
                });
            }
            catch (Exception ex)
            {
                ToastService.ShowError($"Lỗi khi mở modal sửa size: {ex.Message}");
            }
        }

        private void ExpandAll()
        {
            SetExpanded(Items, true);
        }

        private void CollapseAll()
        {
            SetExpanded(Items, false);
        }
        private void SetExpanded(IEnumerable<ITreeViewItem>? items, bool expanded)
        {
            if (items == null) return;

            foreach (var item in items)
            {
                if (item is TreeViewItemDto treeItem)
                {
                    treeItem.Expanded = expanded;

                    if (treeItem.Items != null && treeItem.Items.Any())
                    {
                        SetExpanded(treeItem.Items, expanded);
                    }
                }
            }
        }
        #endregion
        #region category
        private async ValueTask<GridItemsProviderResult<CategoryDto>> LoadCategory(GridItemsProviderRequest<CategoryDto> request)
        {
            try
            {
                var baseQuery = new BaseQuery
                {
                    SearchIn= new List<string> { "CategoryName" },
                    Keyword=KeySearchCategory,
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
                requestRestAPI.Endpoint = "api/Category/GetPaged";

                ResultAPI result = await CallApi.Post<DataTableJson>(requestRestAPI, baseQuery);
                if (result.Status == StatusCode.OK && result.Data is DataTableJson dataTable)
                {
                    var items = JsonSerializer.Deserialize<List<CategoryDto>>(dataTable.Data.GetRawText(),
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<CategoryDto>();



                    var totalRecords = dataTable.RecordsTotal ?? items.Count;
                    return GridItemsProviderResult.From(items, totalRecords);
                }
                StateHasChanged();
                return GridItemsProviderResult.From(new List<CategoryDto>(), 0);
            }
            catch (Exception ex)
            {
                ToastService.ShowError($"Lỗi khi tải danh sách phân loại: {ex.Message}");
                return GridItemsProviderResult.From(new List<CategoryDto>(), 0);
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

        public async Task RejectCategoryAsync(string id)
        {
            requestRestAPI.Endpoint = $"api/Category/{id}/Reject";
            ResultAPI result = await CallApi.Put(requestRestAPI, new object());
            if (result.Status == StatusCode.OK)
            {
                ToastService.ShowSuccess("Huỷ duyệt phân loại thành công");
                await CategoryGrid.RefreshDataAsync();
            }
            else
            {
                ToastService.ShowError("Huỷ duyệt phân loại thất bại!  " + result.Message);
            }

        }
        public async Task ApproveCategoryAsync(string id)
        {
            requestRestAPI.Endpoint = $"api/Category/{id}/Approve";
            ResultAPI result = await CallApi.Put(requestRestAPI, new object());
            if (result.Status == StatusCode.OK)
            {
                ToastService.ShowSuccess("Duyệt phân loại thành công");
                await CategoryGrid.RefreshDataAsync();


            }
            else
            {
                ToastService.ShowError("Duyệt phân loại thất bại!  " + result.Message);
            }

        }
        private async Task DeleteCategoryAsync(string id)
        {
            try
            {
                var dialog = await DialogService.ShowDialogAsync<ModalConfirm>(new DialogParameters());
                var resultDialog = await dialog.Result;
                if (resultDialog.Cancelled == false && resultDialog.Data is bool success && success)
                {
                    requestRestAPI.Endpoint = $"api/Category/{id}";
                    ResultAPI result = await CallApi.Delete(requestRestAPI);
                    if (result.Status == StatusCode.OK)
                    {
                        ToastService.ShowSuccess("Xóa phân loại thành công");
                        await CategoryGrid.RefreshDataAsync();

                    }
                    else
                    {
                        ToastService.ShowError("Xóa phân loại thất bại: " + result.Message);

                    }
                }
            }
            catch (Exception ex)
            {
                ToastService.ShowError("Xóa thất bại: " + ex.Message);
            }

        }
        private async Task OpenModalAddCategory()
        {
            try
            {
                var parameters = new EditOrUpdateParameters
                {
                    IsEditMode = false,
                    OnRefresh = EventCallback.Factory.Create(this, RefreshDataAsync),
                    Data = sizeParent,

                };
                var dialog = await DialogService.ShowDialogAsync<EditCategory>(parameters, new DialogParameters
                {
                    Title = "Thêm phân loại",
                    PreventDismissOnOverlayClick = true,
                    PreventScroll = true,
                    Modal = true
                });
            }
            catch (Exception ex)
            {
                ToastService.ShowError($"Lỗi khi mở modal thêm phân loại: {ex.Message}");
            }
        }
        private async Task OpenModalUpdateCategory(string id)
        {
            try
            {
                var parameters = new EditOrUpdateParameters
                {
                    Id = string.IsNullOrEmpty(id) ? Guid.Empty : Guid.Parse(id),
                    IsEditMode = true,
                    OnRefresh = EventCallback.Factory.Create(this, RefreshDataAsync),
                    Data = sizeParent,

                };
                var dialog = await DialogService.ShowDialogAsync<EditCategory>(parameters, new DialogParameters
                {
                    Title = "Sửa phân loại",
                    PreventDismissOnOverlayClick = true,
                    PreventScroll = true,
                    Modal = true
                });
            }
            catch (Exception ex)
            {
                ToastService.ShowError($"Lỗi khi mở modal sửa phân loại: {ex.Message}");
            }
        }
        private async Task RefreshDataAsync()
        {
            await CategoryGrid.RefreshDataAsync();

        }
        #endregion
    }
}
