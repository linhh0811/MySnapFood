using Service.SnapFood.Share.Model.SQL;

namespace Service.SnapFood.Manage.Dto.Category
{
    public class CategoryDto
    {
        public string Id { get; set; } = string.Empty;
        public int Index { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public ModerationStatus ModerationStatus { get; set; }

    }
}
