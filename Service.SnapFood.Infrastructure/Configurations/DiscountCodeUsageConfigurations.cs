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
    public class DiscountCodeUsageConfigurations : IEntityTypeConfiguration<DiscountCodeUsage>
    {
        public void Configure(EntityTypeBuilder<DiscountCodeUsage> builder)
        {
            builder.ToTable("DiscountCodeUsages");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.UsedAt).IsRequired();

            builder.HasOne(x => x.DiscountCode)
                   .WithMany(x => x.DiscountCodeUsages)
                   .HasForeignKey(x => x.DiscountCodeId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Bill)
                   .WithOne(x => x.DiscountCodeUsages)
                   .HasForeignKey<DiscountCodeUsage>(x => x.BillId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.User)
                   .WithMany(x=>x.DiscountCodeUsage)
                   .HasForeignKey(x => x.UserId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
