using Service.SnapFood.Client.Dto.Size;

namespace Service.SnapFood.Client.Dto.Combo
{
    public class ComboProductDto
    {
        public Guid ProductId { get; set; }
        public string CategoryName { get; set; } = string.Empty;

        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public List<SizeDto>? Sizes { get; set; }
        public string SizeName { get; set; } = string.Empty;


    }
}
