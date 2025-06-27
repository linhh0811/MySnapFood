using Service.SnapFood.Client.Enums;

namespace Service.SnapFood.Client.Dto.Bill
{
    public class BillDto
    {
        public int Index { get; set; }
        public Guid Id { get; set; }
        public string BillCode { get; set; } = string.Empty;
        public Guid UserId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public Guid StoreId { get; set; }

        public StatusOrder Status { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalAmountEndow { get; set; }
        public DateTime Created { get; set; }
    }
}
