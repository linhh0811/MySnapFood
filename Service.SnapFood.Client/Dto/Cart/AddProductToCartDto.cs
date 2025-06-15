namespace Service.SnapFood.Client.Dto.Cart
{
    public class AddProductToCartDto
    {
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
        public Guid SizeId { get; set; }
        public int Quantity { get; set; }
    }
}
