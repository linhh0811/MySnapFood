using Service.SnapFood.Domain.Enums;
using Service.SnapFood.Share.Model.SQL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Service.SnapFood.Domain.Entitys
{
    public class User : BaseDomainEntity
    {
        public Guid? StoreId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string? Numberphone { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public UserType UserType { get; set; }
        public virtual List<Cart> Carts { get; set; } = null!;
        public virtual Store? Store { get; set; }

        public virtual List<UserRole> UserRoles { get; set; } = null!;

        public virtual List<Bill> Orderes { get; set; } = null!;
        public virtual List<Address> Addresses { get; set; } = null!;
        public virtual List<DiscountCodeUsage> DiscountCodeUsage { get; set; } = null!;

    }
}
