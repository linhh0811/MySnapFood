using Service.SnapFood.Share.Model.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.SnapFood.Application.Dtos
{
    public class SizeDto
    {
        public Guid Id { get; set; }
        public string SizeName { get; set; } = string.Empty;
        public decimal AdditionalPrice { get; set; }
        public int DisplayOrder { get; set; }
        public Guid? ParentId { get; set; }
        public ModerationStatus ModerationStatus { get; set; } 
    }
}
