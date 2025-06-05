namespace Service.SnapFood.Client.Dto.Size
{
    public class SizeDto
    {
        public Guid Id { get; set; }
        public string SizeName { get; set; } = string.Empty;
        public decimal AdditionalPrice { get; set; }
        public int DisplayOrder { get; set; }
    }
}
