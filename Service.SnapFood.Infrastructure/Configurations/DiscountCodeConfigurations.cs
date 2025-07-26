using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Service.SnapFood.Domain.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.SnapFood.Infrastructure.Configurations
{
    public class DiscountCodeConfigurations : IEntityTypeConfiguration<DiscountCode>
    {
        public void Configure(EntityTypeBuilder<DiscountCode> builder)
        {
            builder.ToTable("DiscountCodes");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Code).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Description).HasMaxLength(255);
            builder.Property(x => x.DiscountAmount).HasColumnType("decimal(18,2)");
            builder.Property(x => x.MinOrderAmount).HasColumnType("decimal(18,2)");
            builder.Property(x => x.ApplyToOrderTypes).HasMaxLength(100);

            builder.HasMany(x => x.DiscountCodeUsages)
                   .WithOne(x => x.DiscountCode)
                   .HasForeignKey(x => x.DiscountCodeId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
