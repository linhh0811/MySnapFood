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
    public class ProductComboConfigurations : IEntityTypeConfiguration<ProductCombo>
    {
        public void Configure(EntityTypeBuilder<ProductCombo> builder)
        {
            builder.ToTable("ProductCombos");

            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Product)
                .WithMany(x => x.ProductComboes)
                .HasForeignKey(x=>x.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Combo)
                .WithMany(x => x.ProductComboes)
                .HasForeignKey(x=>x.ComboId)
                .OnDelete(DeleteBehavior.Cascade);




        }

    }
}
