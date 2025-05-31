using Service.SnapFood.Share.Model.SQL;
using System.ComponentModel.DataAnnotations;

namespace Service.SnapFood.Client.Dto.Category
{
    public class CategoryDto
    {
        public string? Id { get; set; }
        public int Index { get; set; }  
        public string CategoryName { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public ModerationStatus ModerationStatus { get; set; }
    }
}
