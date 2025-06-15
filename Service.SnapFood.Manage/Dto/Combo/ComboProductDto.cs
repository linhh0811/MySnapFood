using Service.SnapFood.Share.Model.SQL;

namespace Service.SnapFood.Manage.Dto.Combo
{
    public class ComboProductDto
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public ModerationStatus ModerationStatus { get; set; }

    }
}
