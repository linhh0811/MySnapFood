using Service.SnapFood.Manage.Enums;
using Service.SnapFood.Share.Model.Enum;
using System.ComponentModel.DataAnnotations;

namespace Service.SnapFood.Manage.Dto.BillDto
{
    public class BillDto
    {
        public int Index { get; set; }
        public Guid Id { get; set; }
        public string BillCode { get; set; } = string.Empty;
        public Guid UserId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public Guid StoreId { get; set; }
        public string StoreName { get; set; } = string.Empty;
        public StatusOrder  Status { get; set; }
        public string StatusText { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }
        public decimal TotalAmountEndow { get; set; }
        public DateTime Created { get; set; }
        public ReceivingType ReceivingType { get; set; }
        public PhuongThucDatHangEnum PhuongThucDatHang { get; set; }
    }
}
