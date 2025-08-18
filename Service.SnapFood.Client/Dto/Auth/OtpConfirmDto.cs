using System.ComponentModel.DataAnnotations;

namespace Service.SnapFood.Client.Dto.Auth
{
    public class OtpConfirmDto
    {
        [Required(ErrorMessage = "Email không được để trống")]
        [EmailAddress(ErrorMessage = "Email không đúng định dạng")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Mã xác nhận không được để trống")]
        public string OtpCode { get; set; } = string.Empty;

        [Required(ErrorMessage = "Mật khẩu mới không được để trống")]
        public string PasswordMoi { get; set; } = string.Empty;

        [Required(ErrorMessage = "Xác nhận mật khẩu không được để trống")]
        [Compare("PasswordMoi", ErrorMessage = "Mật khẩu xác nhận không khớp")]
        public string PasswordConfirmMoi { get; set; } = string.Empty;
    }
}
