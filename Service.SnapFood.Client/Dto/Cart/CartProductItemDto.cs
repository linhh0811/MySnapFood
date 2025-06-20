namespace Service.SnapFood.Client.Dto.Cart
{
    public class CartProductItemDto
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Guid? SizeId { get; set; }
        public int Quantity { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string SizeName { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}
