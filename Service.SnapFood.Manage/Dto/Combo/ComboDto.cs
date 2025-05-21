using Service.SnapFood.Share.Model.SQL;

namespace Service.SnapFood.Manage.Dto.Combo
{
    public class ComboDto
    {
        public int Index { get; set; }
        public Guid Id { get; set; }

        public Guid CategoryId { get; set; }

        public string ComboName { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public decimal BasePrice { get; set; }
        public string? Description { get; set; }
        public DateTime CreteDate { get; set; }

        public ModerationStatus ModerationStatus { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastModified { get; set; }
        public Guid CreatedBy { get; set; }

        public Guid LastModifiedBy { get; set; }
        public List<ComboProductDto> Products { get; set; } = new();
    }
}
