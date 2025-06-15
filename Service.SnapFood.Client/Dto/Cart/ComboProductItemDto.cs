namespace Service.SnapFood.Client.Dto.Cart
{
    public class ComboProductItemDto
    {
        public Guid ProductId { get; set; }
        public Guid SizeId { get; set; }
        public int Quantity { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string SizeName { get; set; } = string.Empty;
    }
}
