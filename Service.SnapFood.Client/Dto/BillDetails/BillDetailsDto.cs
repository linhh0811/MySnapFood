using Service.SnapFood.Share.Model.Enum;

namespace Service.SnapFood.Client.Dto.BillDetails
{
    public class BillDetailsDto
    {
        public Guid Id { get; set; }
        public string ItemsName { get; set; }
        public string ImageUrl { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal PriceEndow { get; set; }
        public ItemType ItemType { get; set; }
        public List<BillDetailsDto>? Children { get; set; }
    }
}
