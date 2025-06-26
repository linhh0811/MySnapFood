using Service.SnapFood.Domain.Enums;
using Service.SnapFood.Share.Model.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.SnapFood.Application.Dtos
{
    public class PromotionDto
    {
        public Guid Id { get; set; }
        public int Index { get; set; }
        public string PromotionName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public PromotionType PromotionType { get; set; }
        public decimal PromotionValue { get; set; } 
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ModerationStatus ModerationStatus { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastModified { get; set; }
        public Guid CreatedBy { get; set; }

        public Guid LastModifiedBy { get; set; }
        public List<PromotionItemDto> PromotionItems { get; set; } =new List<PromotionItemDto>();
        public string CreatedByName { get; set; } = string.Empty;
        public string LastModifiedByName { get; set; } = string.Empty;
    }
}
