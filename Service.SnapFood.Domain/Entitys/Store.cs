using Service.SnapFood.Domain.Enums;
using Service.SnapFood.Share.Model.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Service.SnapFood.Domain.Entitys
{
    public class Store : BaseDomainEntity
    {
        public string StoreName { get; set; } = string.Empty;
        public Status Status { get; set; }
        public Guid AddressId { get; set; }
        public TimeOnly ThoiGianBatDauHoatDong { get; set; }
        public TimeOnly ThoiGianNgungHoatDong { get; set; }
        public string NumberPhone { get; set; } = string.Empty;

        public virtual Address Address { get; set; } = null!;
        public virtual List<User> Users { get; set; } = null!;

        public virtual List<Bill> Bills { get; set; } = null!;
    }
}
