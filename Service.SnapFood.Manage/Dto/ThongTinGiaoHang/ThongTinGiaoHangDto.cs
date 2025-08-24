using System.ComponentModel.DataAnnotations;

namespace Service.SnapFood.Manage.Dto.ThongTinGiaoHang
{
    public class ThongTinGiaoHangDto
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Bán kính giao hàng không được để trống")]
        [Range(0.1, double.MaxValue, ErrorMessage = "Bán kính giao hàng phải lớn hơn 0 km")]
        public double BanKinhGiaoHang { get; set; } // km

        [Required(ErrorMessage = "Phí giao hàng không được để trống")]
        [Range(0, double.MaxValue, ErrorMessage = "Phí giao hàng không được âm")]
        public decimal PhiGiaoHang { get; set; }

        [Required(ErrorMessage = "Đơn hàng tối thiểu không được để trống")]
        [Range(0, double.MaxValue, ErrorMessage = "Đơn hàng tối thiểu không được âm")]
        public decimal DonHangToiThieu { get; set; }

    }
}
