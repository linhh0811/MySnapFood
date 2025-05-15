using Service.SnapFood.Domain.Enums;
using Service.SnapFood.Share.Model.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.SnapFood.Domain.Entitys
{
    public class BillNotes : IntermediaryEntity
    {
        public Guid BillId { get; set; }
        public NoteType NoteType { get; set; }
        public string NoteContent { get; set; } = string.Empty;
        public Guid CreatedBy { get; set; } 
        public virtual Bill Bill { get; set; } = null!;
    }
}
