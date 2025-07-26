using Service.SnapFood.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.SnapFood.Application.Dtos
{
    public class BillNotesDto
    {
        public Guid Id { get; set; }
        public Guid BillId { get; set; }
        public NoteType NoteType { get; set; }
        public string NoteContent { get; set; } = string.Empty;
        public Guid CreatedBy { get; set; }
        public DateTime Created { get; set; }
    }
}
