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
    public class BillDetailsConfigurations : IEntityTypeConfiguration<BillDetails>
    {
        public void Configure(EntityTypeBuilder<BillDetails> builder)
        {
            builder.ToTable("BillDetails");

            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Bill)
                .WithMany(x => x.BillDetails)
                .HasForeignKey(x => x.BillId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
