using Service.SnapFood.Client.Enums;

namespace Service.SnapFood.Client.Dto.Bill
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
