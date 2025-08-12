using Service.SnapFood.Manage.Enums;

namespace Service.SnapFood.Manage.Dto.Cart
{
    public class QuantityInCartDto
    {
        public Guid UserId { get; set; }
        public Guid ItemId { get; set; }
        public ItemType ItemType { get; set; }
        public int QuantityThayDoi { get; set; }
    }
}
