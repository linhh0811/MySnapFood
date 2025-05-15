using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Service.SnapFood.Domain.Entitys;

namespace Service.SnapFood.Infrastructure.Configurations
{
    public class BillPaymentConfigurations : IEntityTypeConfiguration<BillPayment>
    {
        public void Configure(EntityTypeBuilder<BillPayment> builder)
        {
            builder.ToTable("BillPayments");

            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Bill)
                .WithMany(x => x.BillPayments)
                .HasForeignKey(x => x.BillId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
