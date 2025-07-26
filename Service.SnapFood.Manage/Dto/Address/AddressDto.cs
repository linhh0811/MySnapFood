namespace Service.SnapFood.Manage.Dto.Address
{
    public class AddressDto
    {

        public Guid Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string NumberPhone { get; set; } = string.Empty;
        public string Province { get; set; } = string.Empty;
        public string District { get; set; } = string.Empty;
        public string Ward { get; set; } = string.Empty;
        public string SpecificAddress { get; set; } = string.Empty;
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string FullAddress { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

    }
}
