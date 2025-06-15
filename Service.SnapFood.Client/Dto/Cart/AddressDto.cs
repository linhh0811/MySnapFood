namespace Service.SnapFood.Client.Dto.Cart
{
    public class AddressDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string NumberPhone { get; set; } = string.Empty;
        public string FullAddress { get; set; } = string.Empty;
    }
}
