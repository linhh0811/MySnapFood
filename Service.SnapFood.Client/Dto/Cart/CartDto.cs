namespace Service.SnapFood.Client.Dto.Cart
{
    public class CartDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public int TotalQuantity { get; set; }
        public decimal TotalPrice { get; set; }
        public List<CartProductItemDto> CartProductItems { get; set; } = new List<CartProductItemDto>();
        public List<CartComboItemDto> CartComboItems { get; set; } = new List<CartComboItemDto>();
    }
}
