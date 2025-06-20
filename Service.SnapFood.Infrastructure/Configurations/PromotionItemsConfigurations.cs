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
    public class PromotionItemsConfigurations : IEntityTypeConfiguration<PromotionItem>
    {
        public void Configure(EntityTypeBuilder<PromotionItem> builder)
        {
            builder.ToTable("PromotionItem");

            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Combo)
                .WithMany(x => x.PromotionItems)
                .HasForeignKey(x => x.ComboId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Product)
               .WithMany(x => x.PromotionItems)
               .HasForeignKey(x => x.ProductId)
               .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Promotion)
               .WithMany(x => x.PromotionItems)
               .HasForeignKey(x => x.PromotionId)
               .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
