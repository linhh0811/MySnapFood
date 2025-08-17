using Service.SnapFood.Domain.Enums;
using Service.SnapFood.Share.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.SnapFood.Application.Dtos
{
    public class BillViewDto
    {
        public Guid Id { get; set; }
        public string BillCode { get; set; } = string.Empty;
        public StatusOrder Status { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalAmountEndow { get; set; }
        public decimal DiscountAmount { get; set; }
        public PhuongThucDatHangEnum PhuongThucDatHang { get; set; }
        public ReceivingType ReceivingType { get; set; }


        public DateTime Created {  get; set; }

        public List<BillDetailsDto> BillDetailsDtos { get; set; } = new List<BillDetailsDto>();
        public BillPaymentDto BillPaymentDto { get; set; } = new BillPaymentDto();
        public List<BillNotesDto> BillNotesDtos { get; set; } = new List<BillNotesDto>();
        public BillDeliveryDto BillDeliveryDto { get; set; } = new BillDeliveryDto();
    }
}
