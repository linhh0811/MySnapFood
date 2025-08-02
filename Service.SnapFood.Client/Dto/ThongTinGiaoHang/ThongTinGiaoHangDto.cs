namespace Service.SnapFood.Client.Dto.ThongTinGiaoHang
{
    public class ThongTinGiaoHangDto
    {
        public Guid Id { get; set; }
        public double BanKinhGiaoHang { get; set; }//km
        public decimal PhiGiaoHang { get; set; }
        public decimal DonHangToiThieu { get; set; }
    }
}
