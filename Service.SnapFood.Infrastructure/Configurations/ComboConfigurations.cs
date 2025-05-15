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
    public class ComboConfigurations : IEntityTypeConfiguration<Combo>
    {
        public void Configure(EntityTypeBuilder<Combo> builder)
        {
            builder.ToTable("Combos");

            builder.HasKey(x => x.Id);

            builder.HasOne(x=>x.Category)
                .WithOne(x => x.Combo)
                .HasForeignKey<Combo>(x=>x.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);


        }

    }
}
