using Service.SnapFood.Share.Model.SQL;

namespace Service.SnapFood.Manage.Dto.SizeDto
{
    public class SizeTreeDto
    {
        public string Id { get; set; } = string.Empty;
        public string? ParentId { set; get; }
        public decimal? AdditionalPrice { get; set; }

        public string SizeName { set; get; } = string.Empty;
        public int DisplayOrder { get; set; }
        public List<SizeTreeDto> Children { set; get; } = new();
        public ModerationStatus ModerationStatus { get; set; }
    }
}
