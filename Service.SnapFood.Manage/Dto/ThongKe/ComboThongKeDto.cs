namespace Service.SnapFood.Manage.Dto.ThongKe
{
    public class ComboThongKeDto
    {
        public Guid Id { get; set; }
        public string ComboName { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;

        public int Quantity { get; set; }
    }
}
