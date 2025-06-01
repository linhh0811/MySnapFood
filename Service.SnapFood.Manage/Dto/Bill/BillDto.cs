namespace Service.SnapFood.Manage.Dto.Bill
{
    public class BillDto
    {
        public int Index { get; set; }
        public Guid Id { get; set; }
        public string BillCode { get; set; } = string.Empty;
        public Guid UserId { get; set; }
        public Guid StoreId { get; set; }
        public int Status { get; set; } // Sử dụng int thay vì StatusOrder vì frontend không cần enum từ backend
        public decimal TotalAmount { get; set; }
        public decimal TotalAmountEndow { get; set; }
        public DateTime Created { get; set; }
    }
}