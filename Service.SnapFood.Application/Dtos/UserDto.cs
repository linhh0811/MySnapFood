using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.SnapFood.Share.Model.SQL;
using Service.SnapFood.Domain.Enums;
namespace Service.SnapFood.Application.Dtos
{
    public class UserDto
    {
        public int Index { get; set; }
        public Guid Id { get; set; }
        public Guid? StoreId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Numberphone { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string PasswordMoi { get; set; } = string.Empty;
        public string PasswordConfirmMoi { get; set; } = string.Empty;

        public UserType UserType { get; set; }
        public bool IsInRole { get; set; }
        public ModerationStatus ModerationStatus { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastModified { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid LastModifiedBy { get; set; }
        public string CreatedByName { get; set; } = string.Empty;
        public string LastModifiedByName { get; set; } = string.Empty;
        public bool IsThayDoiMatKhau { get; set; } = false;
    }
}