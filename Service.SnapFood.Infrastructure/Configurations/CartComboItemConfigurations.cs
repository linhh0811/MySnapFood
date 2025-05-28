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
    public class CartComboItemConfigurations : IEntityTypeConfiguration<CartComboItem>
    {
        public void Configure(EntityTypeBuilder<CartComboItem> builder)
        {
            builder.ToTable("CartComboItems");

            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Cart)
                .WithMany(x => x.CartComboItems)
                .HasForeignKey(x => x.CartId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Combo)
                .WithMany(x => x.CartItemes)
                .HasForeignKey(x => x.ComboId)
                .OnDelete(DeleteBehavior.Cascade);

         

        }

    }
}
