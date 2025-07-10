namespace Service.SnapFood.Client.Dto.Cart
{
    public class StoreDto
    {
        public Guid Id { get; set; }
        public string StoreName { get; set; } = string.Empty;
        public AddressDto Address { get; set; } = new AddressDto();
    }
}
