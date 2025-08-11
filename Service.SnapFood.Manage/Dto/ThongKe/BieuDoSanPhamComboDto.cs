namespace Service.SnapFood.Manage.Dto.ThongKe
{
    public class BieuDoSanPhamComboDto
    {
        public List<SanPhamComboDto> SanPhamComboCount { get; set; } = new();
        public List<SanPhamThongKeDto> SanPhamThongKe { get; set; } = new();
        public List<ComboThongKeDto> ComboThongKeDto { get; set; } = new();

    }
}
