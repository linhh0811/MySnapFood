namespace Service.SnapFood.Client.Dto.Cart
{
    public class AddComboToCartDto
    {
        public Guid UserId { get; set; }
        public Guid ComboId { get; set; }
        public List<ComboProductSizeDto> ProductSizes { get; set; } = new List<ComboProductSizeDto>();
        public int Quantity { get; set; }
    }
}
