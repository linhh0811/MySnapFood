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
    public class ComboItemsArchiveConfigurations : IEntityTypeConfiguration<ComboItemsArchive>
    {
        public void Configure(EntityTypeBuilder<ComboItemsArchive> builder)
        {
            builder.ToTable("ComboItemsArchives");

            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.BillDetails)
                .WithMany(x => x.ComboItemsArchives)
                .HasForeignKey(x => x.BillDetailsId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
