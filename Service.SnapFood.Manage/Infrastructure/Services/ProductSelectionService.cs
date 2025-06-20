using Service.SnapFood.Manage.Dto.ProductDto;

namespace Service.SnapFood.Manage.Infrastructure.Services
{
    public class ProductSelectionService
    {
        private List<ProductDto> _allProducts = new();
        private List<ProductDto> _selectedProducts = new();

        public void Initialize(List<ProductDto> products)
        {
            _allProducts = products;

            // Khởi tạo số lượng mặc định là 1 cho tất cả sản phẩm
            foreach (var product in _allProducts)
            {
                product.Quantity = 1;

                // Đồng bộ với sản phẩm đã chọn trước đó
                var existing = _selectedProducts.FirstOrDefault(p => p.Id == product.Id);
                if (existing != null)
                {
                    product.IsSelected = true;
                    product.Quantity = existing.Quantity > 0 ? existing.Quantity : 1;
                }
            }
        }

        public void ToggleProductSelection(ProductDto product, bool isSelected)
        {
            if (isSelected)
            {
                // Đảm bảo số lượng tối thiểu là 1 khi chọn
                product.Quantity = product.Quantity > 0 ? product.Quantity : 1;

                if (!_selectedProducts.Any(p => p.Id == product.Id))
                {
                    _selectedProducts.Add(new ProductDto
                    {
                        Id = product.Id,
                        ProductName = product.ProductName,
                        ImageUrl = product.ImageUrl,
                        BasePrice = product.BasePrice,
                        Quantity = product.Quantity > 0 ? product.Quantity : 1,
                        IsSelected = true
                    });
                }
            }
            else
            {
                _selectedProducts.RemoveAll(p => p.Id == product.Id);
            }

            // Đồng bộ với danh sách chính
            var allProduct = _allProducts.FirstOrDefault(p => p.Id == product.Id);
            if (allProduct != null)
            {
                allProduct.Quantity = product.Quantity;
                allProduct.IsSelected = isSelected;
            }
        }

        public void RemoveProduct(ProductDto product)
        {
            _selectedProducts.RemoveAll(p => p.Id == product.Id);
            var allProduct = _allProducts.FirstOrDefault(p => p.Id == product.Id);
            if (allProduct != null)
            {
                allProduct.IsSelected = false;
            }
        }

        public void UpdateQuantity(ProductDto product, int newQuantity)
        {
            // Cập nhật trong danh sách đã chọn
            var selected = _selectedProducts.FirstOrDefault(p => p.Id == product.Id);
            if (selected != null)
            {
                selected.Quantity = newQuantity > 0 ? newQuantity : 1;
            }

            // Cập nhật trong danh sách chính
            var allProduct = _allProducts.FirstOrDefault(p => p.Id == product.Id);
            if (allProduct != null)
            {
                allProduct.Quantity = newQuantity > 0 ? newQuantity : 1;
            }
        }
        // Method mới để khởi tạo với danh sách đã chọn sẵn (cho trường hợp edit)
        public void InitializeWithSelected(List<ProductDto> selectedProducts)
        {
            _selectedProducts = selectedProducts?.Select(p => new ProductDto
            {
                Id = p.Id,
                ProductName = p.ProductName,
                ImageUrl = p.ImageUrl,
                BasePrice = p.BasePrice,
                Quantity = p.Quantity > 0 ? p.Quantity : 1,
                IsSelected = true,
                ModerationStatus=p.ModerationStatus
            }).ToList() ?? new List<ProductDto>();
        }

        public IReadOnlyList<ProductDto> SelectedProducts => _selectedProducts;
    }
}
