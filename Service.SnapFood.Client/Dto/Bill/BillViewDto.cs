using Service.SnapFood.Client.Dto.BillDetails;
using Service.SnapFood.Client.Enums;

namespace Service.SnapFood.Client.Dto.Bill
{
    public class BillViewDto
    {
        public Guid Id { get; set; }
        public string BillCode { get; set; } = string.Empty;
        public StatusOrder Status { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalAmountEndow { get; set; }
        public decimal DiscountAmount { get; set; }

        public DateTime Created { get; set; }

        public List<BillDetailsDto> BillDetailsDtos { get; set; } = new List<BillDetailsDto>();
        public BillPaymentDto BillPaymentDto { get; set; } = new BillPaymentDto();
        public List<BillNotesDto> BillNotesDtos { get; set; } = new List<BillNotesDto>();
        public BillDeliveryDto BillDeliveryDto { get; set; } = new BillDeliveryDto();
    }
}
