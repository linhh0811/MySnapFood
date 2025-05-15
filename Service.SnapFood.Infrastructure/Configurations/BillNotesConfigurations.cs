using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Service.SnapFood.Domain.Entitys;

namespace Service.SnapFood.Infrastructure.Configurations
{
    public class BillNotesConfigurations : IEntityTypeConfiguration<BillNotes>
    {
        public void Configure(EntityTypeBuilder<BillNotes> builder)
        {
            builder.ToTable("BillNotes");

            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Bill)
                .WithMany(x => x.BillNotes)
                .HasForeignKey(x => x.BillId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
