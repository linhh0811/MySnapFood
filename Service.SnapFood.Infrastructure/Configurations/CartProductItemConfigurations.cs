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
    public class CartProductItemConfigurations : IEntityTypeConfiguration<CartProductItem>
    {
        public void Configure(EntityTypeBuilder<CartProductItem> builder)
        {
            builder.ToTable("CartProductItems");

            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Cart)
                .WithMany(x => x.CartProductItems)
                .HasForeignKey(x => x.CartId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Product)
                .WithMany(x => x.CartItemes)
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Size)
                .WithMany(x => x.CartProductItem)
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

        }

    }
}
