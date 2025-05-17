using Service.SnapFood.Share.Model.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.SnapFood.Application.Dtos
{
    public class SizeTreeDto
    {
        public Guid Id { get; set; }
        public Guid? ParentId { set; get; }
        public decimal? AdditionalPrice { get; set; }

        public string SizeName { set; get; } = string.Empty;
        public int DisplayOrder { get; set; }
        public List<SizeTreeDto> Children { set; get; } = new();
        public ModerationStatus ModerationStatus { get; set; }
    }
}
