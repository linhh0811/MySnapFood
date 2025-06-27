
using Service.SnapFood.Manage.Query;
using Service.SnapFood.Share.Model.SQL;
using System.ComponentModel.DataAnnotations;

namespace Service.SnapFood.Manage.Dto.Promotion
{
    public class PromotionDto
    {
        public Guid Id { get; set; }
        public int Index { get; set; }
        [Required(ErrorMessage = "Tên khuyến mại không để trống")]
        public string PromotionName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public PromotionType PromotionType { get; set; }
        [Required(ErrorMessage = "Giá trị khuyến mại không để trống")]
        [Range(1, int.MaxValue, ErrorMessage = "Giá trị lớn hơn 0")]

        public decimal PromotionValue { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ModerationStatus ModerationStatus { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastModified { get; set; }
        public Guid CreatedBy { get; set; }

        public Guid LastModifiedBy { get; set; }
        public string CreatedByName { get; set; } = string.Empty;
        public string LastModifiedByName { get; set; } = string.Empty;
        public List<PromotionItemDto> PromotionItems { get; set; } = new List<PromotionItemDto>();


    }
}
