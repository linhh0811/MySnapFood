using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using Service.SnapFood.Manage.Dto.Combo;
using Service.SnapFood.Share.Interface.Extentions;
using Service.SnapFood.Share.Model.ServiceCustomHttpClient;
using Service.SnapFood.Share.Query.Grid;
using Service.SnapFood.Share.Query;
using System.Text.Json;
using Service.SnapFood.Manage.Dto;

namespace Service.SnapFood.Manage.Components.Pages.Manage.Combo
{
    public partial class Index : ComponentBase
    {
        [Inject] private ICallServiceRegistry CallApi { get; set; } = default!;
        [Inject] private IToastService ToastService { get; set; } = default!;
        [Inject] private IDialogService DialogService { get; set; } = default!;
        private ApiRequestModel requestRestAPI = new ApiRequestModel();
        protected FluentDataGrid<ComboDto> ComboGrid { get; set; } = default!;
        protected string SearchKeyword { get; set; } = string.Empty;
        private PaginationState pagination = new PaginationState { ItemsPerPage = 10 };
        private async ValueTask<GridItemsProviderResult<ComboDto>> LoadCombo(GridItemsProviderRequest<ComboDto> request)
        {
            try
            {
                var baseQuery = new BaseQuery
                {
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
    }
}
