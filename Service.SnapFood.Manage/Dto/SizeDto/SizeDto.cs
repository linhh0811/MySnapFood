using Service.SnapFood.Share.Model.SQL;

namespace Service.SnapFood.Manage.Dto.SizeDto
{
    public class SizeDto
    {
        public string Id { get; set; }
        public string SizeName { get; set; } = string.Empty;
        public decimal AdditionalPrice { get; set; }
        public int DisplayOrder { get; set; }
        public Guid? ParentId { get; set; }
        public ModerationStatus ModerationStatus { get; set; }
    }
}
