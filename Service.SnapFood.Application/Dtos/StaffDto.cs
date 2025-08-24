using Service.SnapFood.Share.Model.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.SnapFood.Application.Dtos
{
    public class StaffDto
    {
        public int Index { get; set; }
        public string? Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Numberphone { get; set; } = string.Empty;
        public ModerationStatus ModerationStatus { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastModified { get; set; }
        public string CreatedByName { get; set; } = string.Empty;
        public string LastModifiedByName { get; set; } = string.Empty;
        public bool IsHeThong { get; set; } = false;
    }
}
