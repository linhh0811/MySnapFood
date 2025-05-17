using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Service.SnapFood.Domain.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.SnapFood.Infrastructure.Configurations
{
    public class ProductConfigurations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Category).WithOne(x => x.Product)
               .HasForeignKey<Product>(x => x.CategoryId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x=>x.Size).WithOne(x => x.Product)
                .HasForeignKey<Product>(x => x.SizeId)
                .OnDelete(DeleteBehavior.Restrict);
        }

    }
}
