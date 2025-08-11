namespace Service.SnapFood.Manage.Dto.ThongKe
{
    public class SanPhamThongKeDto
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;

        public int Quantity { get; set; }
    }
}
