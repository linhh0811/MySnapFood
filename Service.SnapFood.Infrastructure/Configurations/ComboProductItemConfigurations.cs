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
    public class ComboProductItemConfigurations : IEntityTypeConfiguration<ComboProductItem>
    {
        public void Configure(EntityTypeBuilder<ComboProductItem> builder)
        {
            builder.ToTable("ComboProductItem");

            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.CartComboItem)
              .WithMany(x => x.ComboProductItems)
              .HasForeignKey(x => x.CartComboId)
              .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Size)
             .WithMany(x => x.ComboProductItem)
             .HasForeignKey(x => x.SizeId)
             .OnDelete(DeleteBehavior.Cascade).IsRequired(false);

            builder.HasOne(x => x.Product)
             .WithMany(x => x.ComboProductItem)
             .HasForeignKey(x => x.ProductId)
             .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
