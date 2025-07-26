﻿using Service.SnapFood.Domain.Enums;
using Service.SnapFood.Share.Model.SQL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.SnapFood.Domain.Entitys
{
    public class Address : BaseDomainEntity
    {
        public Guid? UserId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string NumberPhone { get; set; } = string.Empty;
        public string Province { get; set; } = string.Empty;//tỉnh
        public string District { get; set; } = string.Empty;//huyện
        public string Ward { get; set; } = string.Empty;//xã
        public string SpecificAddress { get; set; } = string.Empty;//địa chỉ cụ thể

        public double Latitude { get; set; } //vĩ độ
        public double Longitude { get; set; } // kinh độ
        public string FullAddress { get; set; } = string.Empty;
        public AddressType AddressType { get; set; }
        public string Description { get; set; } = string.Empty;


        public virtual User User { get; set; } = null!;
        public virtual Store Store { get; set; } = null!;
    }
}
