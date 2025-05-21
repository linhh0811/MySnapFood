using Service.SnapFood.Domain.Enums;
using Service.SnapFood.Share.Model.SQL;
using System;

namespace Service.SnapFood.Application.Dtos
{
    public class AddressDto
    {
        public int Index { get; set; }
        public Guid Id { get; set; }
        public Guid? UserId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string NumberPhone { get; set; } = string.Empty;
        public string Province { get; set; } = string.Empty;
        public string District { get; set; } = string.Empty;
        public string Ward { get; set; } = string.Empty;
        public string SpecificAddress { get; set; } = string.Empty;
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string FullAddress { get; set; } = string.Empty;
        public AddressType AddressType { get; set; }
        public ModerationStatus ModerationStatus { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastModified { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid LastModifiedBy { get; set; }
    }
}