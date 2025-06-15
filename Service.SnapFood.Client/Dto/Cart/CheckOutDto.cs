namespace Service.SnapFood.Client.Dto.Cart
{
    public class CheckOutDto
    {
        public Guid UserId { get; set; }
        public Guid AddressId { get; set; }
        public Guid StoreId { get; set; }
        public string PaymentMethod { get; set; } = string.Empty;
    }
}
