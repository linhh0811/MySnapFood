using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.SnapFood.Application.Dtos
{
    public class OtpConfirmDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }

        public string Email { get; set; } = string.Empty;


        public string OtpCode { get; set; } = string.Empty;

        public string PasswordMoi { get; set; } = string.Empty;

        public string PasswordConfirmMoi { get; set; } = string.Empty;
    }
}
