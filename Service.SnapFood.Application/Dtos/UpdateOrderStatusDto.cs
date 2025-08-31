using Service.SnapFood.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.SnapFood.Application.Dtos
{
    public class UpdateOrderStatusDto
    {
        public StatusOrder StatusOrder { get; set; }
        public string Reason { get; set; } = string.Empty;
        public string NhanVienThaoTac { get; set; } = string.Empty;

    }
}
