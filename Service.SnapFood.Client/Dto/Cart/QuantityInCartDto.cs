using Service.SnapFood.Client.Enums;

namespace Service.SnapFood.Client.Dto.Cart
{
    public class QuantityInCartDto
    {
        public Guid UserId { get; set; }
        public Guid ItemId { get; set; }
        public ItemType ItemType { get; set; }
        public int QuantityThayDoi { get; set; }
    }
}
