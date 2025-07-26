using Service.SnapFood.Client.Dto.Bill;
using Service.SnapFood.Client.Enums;

namespace Service.SnapFood.Client.Dto.BillDetails
{
    public class BillDetailsDto
    {
        public Guid Id { get; set; }
        public Guid BillId { get; set; }

        public string ItemsName { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal PriceEndow { get; set; }
        public ItemType ItemType { get; set; }
        public List<ComboItemsArchiveDto>? Product { get; set; }
    }
}
