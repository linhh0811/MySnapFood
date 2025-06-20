namespace Service.SnapFood.Client.Dto.Cart
{
    public class CartComboItemDto
    {
        public Guid Id { get; set; }
        public Guid ComboId { get; set; }
        public Guid SizeId { get; set; }
        public int Quantity { get; set; }
        public string ComboName { get; set; } = string.Empty;
        public string SizeName { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public List<ComboProductItemDto> ComboProductItems { get; set; } = new List<ComboProductItemDto>();
    }
}
