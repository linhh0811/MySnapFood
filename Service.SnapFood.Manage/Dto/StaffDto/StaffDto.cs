using System;
using System.ComponentModel.DataAnnotations;

namespace Service.SnapFood.Manage.Dto.StaffDto
{
    public class StaffDto
    {
        public int Index { get; set; }
        public string? Id { get; set; }
        [Required(ErrorMessage ="Tên nhân viên không được để trống")]
        public string FullName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Email không được để trống")]
        [EmailAddress(ErrorMessage ="Email không hợp lệ!")]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage ="SĐT không được để trống")]
        [RegularExpression(@"^0\d{9}$", ErrorMessage ="SĐT không hợp lệ!")]
        public string Numberphone { get; set; } = string.Empty;
        public int ModerationStatus { get; set; }
    }
}
