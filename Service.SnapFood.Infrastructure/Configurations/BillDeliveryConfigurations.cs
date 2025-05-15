using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Service.SnapFood.Domain.Entitys;

namespace Service.SnapFood.Infrastructure.Configurations
{
    public class BillDeliveryConfigurations : IEntityTypeConfiguration<BillDelivery>
    {
        public void Configure(EntityTypeBuilder<BillDelivery> builder)
        {
            builder.ToTable("BillDelivery");

            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Bill)
                .WithOne(x => x.BillDelivery)
                .HasForeignKey<BillDelivery>(x => x.BillId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
