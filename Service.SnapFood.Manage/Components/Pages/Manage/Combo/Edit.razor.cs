using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.FluentUI.AspNetCore.Components;
using Service.SnapFood.Manage.Dto;
using Service.SnapFood.Manage.Dto.Category;
using Service.SnapFood.Manage.Dto.Combo;
using Service.SnapFood.Manage.Infrastructure.Services;
using Service.SnapFood.Share.Interface.Extentions;
using Service.SnapFood.Share.Model.ServiceCustomHttpClient;
using Service.SnapFood.Share.Model.SQL;
using Service.SnapFood.Share.Query.Grid;
using Service.SnapFood.Share.Query;
using System.Text.Json;
using Service.SnapFood.Manage.Dto.ProductDto;
using System.Threading.Tasks;

namespace Service.SnapFood.Manage.Components.Pages.Manage.Combo
{
    public partial class Edit : ComponentBase
    {
        [Inject] private ICallServiceRegistry CallApi { get; set; } = default!;
        [Inject] private IToastService ToastService { get; set; } = default!;
        [Inject] private ImageService ImageUploadService { get; set; } = default!;
        [CascadingParameter] public FluentDialog Dialog { get; set; } = default!;
        [Parameter] public EditOrUpdateParameters Content { get; set; } = new();
        private ApiRequestModel requestRestAPI = new ApiRequestModel() { };
        private ComboDto ComboModel { get; set; } = new ComboDto();
        private List<CategoryDto> CategoryList { get; set; } = new List<CategoryDto>();
        private string? ErrorMessage;
        private bool isSaving = false;
        private string imagePreviewUrl = string.Empty;
        private IBrowserFile ImageFile { get; set; } = default!;
        protected FluentDataGrid<ProductDto> ProductGrid { get; set; } = default!;
        protected string SearchKeyword { get; set; } = string.Empty;
        private PaginationState pagination = new PaginationState { ItemsPerPage = 10 };
        private List<ProductDto> productDtos = new List<ProductDto>();
        private ProductSelectionService _productSelectionManager = new ProductSelectionService();
        private int TotalQuantity => productDtos.Sum(p => p.Quantity);
        private decimal TotalPrice => productDtos.Sum(p => p.BasePrice * p.Quantity);
        private async Task FileSelect(InputFileChangeEventArgs e)
        {
            ImageFile = e.File;
            ComboModel.ImageUrl = "A";
            var format = "image/jpeg";
            using var imageFile = e.File.OpenReadStream(maxAllowedSize: 5 * 1024 * 1024); // Giới hạn 5MB
            var memoryStream = new MemoryStream();

            await imageFile.CopyToAsync(memoryStream);

            imagePreviewUrl = $"data:{format};base64,{Convert.ToBase64String(memoryStream.ToArray())}";
        }

        protected override async Task OnInitializedAsync()
        {
            await GetCategory();
            if (Content.IsEditMode)
            {
                await GetCombo();
            }
            _productSelectionManager.Initialize(new List<ProductDto>());
           
        }
       
        #region get dữ liệu
        private async Task GetCategory()
        {
            ApiRequestModel requestRestAPI = new ApiRequestModel() { };
            requestRestAPI.Endpoint = $"api/Category/";
            ResultAPI result = await CallApi.Get<List<CategoryDto>>(requestRestAPI);
            if (result.Status == StatusCode.OK)
            {
                CategoryList = result.Data as List<CategoryDto> ?? new List<CategoryDto>();
                ComboModel.CategoryId = CategoryList.First().Id??string.Empty;
            }
        }
        private async Task GetCombo()
        {
            ApiRequestModel requestRestAPI = new ApiRequestModel() { };
            requestRestAPI.Endpoint = $"api/Combo/{Content.Id}";
            ResultAPI result = await CallApi.Get<ComboDto>(requestRestAPI);
            if (result.Status == StatusCode.OK)
            {
                var ComboModelResult = result.Data as ComboDto ?? new ComboDto();
               
                ComboModel.ComboName = ComboModelResult.ComboName;
                ComboModel.CategoryId = ComboModelResult.CategoryId;
                ComboModel.Description = ComboModelResult.Description;
                ComboModel.ComboItems = ComboModelResult.ComboItems;
                ComboModel.ImageUrl = ComboModelResult.ImageUrl;
                imagePreviewUrl = ComboModel.ImageUrl;

                if (ComboModel.ComboItems?.Any() == true)
                {
                    await LoadComboItems();
                }

            }
            _productSelectionManager.Initialize(new List<ProductDto>());
            await ProductGrid.RefreshDataAsync();

        }
        private async Task LoadComboItems()
        {
            try
            {
                // Load thông tin chi tiết các sản phẩm trong combo
                var productIds = ComboModel.ComboItems.Select(ci => ci.ProductId).ToList();

                foreach (var comboItem in ComboModel.ComboItems)
                {
                    requestRestAPI.Endpoint = $"api/Product/{comboItem.ProductId}";
                    ResultAPI result = await CallApi.Get<ProductDto>(requestRestAPI);

                    if (result.Status == StatusCode.OK && result.Data is ProductDto product)
                    {
                        product.Quantity = comboItem.Quantity;
                        product.IsSelected = true;
                        productDtos.Add(product);
                    }
                }

                // Cập nhật ProductSelectionService với dữ liệu đã load
                _productSelectionManager.InitializeWithSelected(productDtos);
            }
            catch (Exception ex)
            {
                ToastService.ShowError($"Lỗi khi tải dữ liệu combo: {ex.Message}");
            }
        }
        #endregion

        #region create
        private async Task<bool> CreateCombo(ComboDto createRequest)
        {
            ErrorMessage = null;
            try
            {
                isSaving = true;
                var fileName = await ImageUploadService.SaveImageAsync(ImageFile);
                createRequest.ImageUrl = ImageUploadService.GetImageUrl(fileName);
                createRequest.ComboItems = productDtos.Select(p => new ComboProductDto
                {
                    ProductId = p.Id,
                    Quantity = p.Quantity
                }).ToList();
                createRequest.BasePrice = TotalPrice;   
                requestRestAPI.Endpoint = "api/Combo";
                ResultAPI result = await CallApi.Post<ComboDto>(requestRestAPI, createRequest);
                if (result.Status == StatusCode.OK)
                {
                    ToastService.ShowSuccess("Thêm combo thành công.");
                    return true;
                }
                else
                {
                    isSaving = false;
                    ErrorMessage = "Thêm combo thất bại: " + result.Message;
                    return false;
                }
            }
            catch (Exception ex)
            {
                isSaving = false;
                ErrorMessage = "Thêm combo thất bại: " + ex.Message;
                return false;
            }
        }
        #endregion

        #region update
        private async Task<bool> UpdateCombo(Guid id, ComboDto updateRequest)
        {
            ErrorMessage = null;
            try
            {
                isSaving = true;
                if (ImageFile is not null)
                {
                    var fileName = await ImageUploadService.SaveImageAsync(ImageFile);
                    updateRequest.ImageUrl = ImageUploadService.GetImageUrl(fileName);
                }

                // Cập nhật combo items
                updateRequest.ComboItems = productDtos.Select(p => new ComboProductDto
                {
                    ProductId = p.Id,
                    Quantity = p.Quantity
                }).ToList();

                updateRequest.BasePrice = TotalPrice;

                
                requestRestAPI.Endpoint = $"api/Combo/{id}";
                ResultAPI result = await CallApi.Put(requestRestAPI, updateRequest);

                if (result.Status == StatusCode.OK)
                {
                    ToastService.ShowSuccess("Sửa combo thành công.");
                    return true;
                }
                else
                {
                    isSaving = false;
                    ErrorMessage = "Sửa combo thất bại: " + result.Message;
                    return false;
                }
            }
            catch (Exception ex)
            {
                isSaving = false;
                ErrorMessage = "Sửa combo thất bại: " + ex.Message;
                return false;
            }
        }
        #endregion
        private async Task HandleSubmit()
        {

            bool result = false;

            if (Content.IsEditMode)
            {
                result = await UpdateCombo(Content.Id, ComboModel);
            }
            else
            {
                result = await CreateCombo(ComboModel);
            }

            if (result)
            {
                await Dialog.CloseAsync();
                await Content.OnRefresh.InvokeAsync();
            }

        }
        public async Task CancelAsync()
        {
            await Dialog.CancelAsync();
        }
        #region danh sách sản phẩm
        private async ValueTask<GridItemsProviderResult<ProductDto>> LoadProduct(GridItemsProviderRequest<ProductDto> request)
        {
            try
            {
                var baseQuery = new BaseQuery
                {
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
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<ProductDto>();
                    items.ForEach(p => p.Quantity = 1);
                    // Cập nhật trình quản lý với các sản phẩm mới
                    _productSelectionManager.Initialize(items);

                    // Khôi phục trạng thái chọn cho các sản phẩm đã chọn trước đó
                    foreach (var selected in productDtos)
                    {
                        var item = items.FirstOrDefault(p => p.Id == selected.Id);
                        if (item != null)
                        {
                            item.IsSelected = true;
                            item.Quantity = selected.Quantity; // Đồng bộ số lượng
                        }
                    }


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
        private async Task RefreshDataAsync()
        {
            await ProductGrid.RefreshDataAsync();
        }
        #endregion
        private async Task HandleProductSelection(ProductDto product, bool isSelected)
        {
            // Đảm bảo thay đổi trạng thái trước khi cập nhật
            product.IsSelected = isSelected;

            _productSelectionManager.ToggleProductSelection(product, isSelected);
            productDtos = _productSelectionManager.SelectedProducts.ToList();

            // Force refresh cả grid
            await ProductGrid.RefreshDataAsync();
            StateHasChanged();
        }
        private async Task  RemoveProduct(ProductDto product)
        {
            _productSelectionManager.RemoveProduct(product);

            // Cập nhật danh sách cục bộ
            productDtos = _productSelectionManager.SelectedProducts.ToList();
            await ProductGrid.RefreshDataAsync();
            StateHasChanged();
        }
        private void HandleQuantityChange(ProductDto product, int newQuantity)
        {
            // Cập nhật số lượng trong danh sách đã chọn
            var selectedProduct = productDtos.FirstOrDefault(p => p.Id == product.Id);
            if (selectedProduct != null)
            {
                selectedProduct.Quantity = newQuantity;
            }

            // Cập nhật số lượng trong danh sách chính
            _productSelectionManager.UpdateQuantity(product, newQuantity);

            StateHasChanged();
        }
        string GetPriceStyle(ProductDto context)
        {
            if (context.ModerationStatus == ModerationStatus.Rejected)
            {
                return "cursor: pointer; color: gray; text-decoration: line-through;";
            }
            return "cursor: pointer;";
        }
    }
}
