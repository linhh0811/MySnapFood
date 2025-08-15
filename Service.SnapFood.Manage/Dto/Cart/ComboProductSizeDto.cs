namespace Service.SnapFood.Manage.Dto.Cart
{
    public class ComboProductSizeDto
    {
        public Guid ProductId { get; set; }
        public Guid? SizeId { get; set; }
        public int Quantity { get; set; }
    }
}
