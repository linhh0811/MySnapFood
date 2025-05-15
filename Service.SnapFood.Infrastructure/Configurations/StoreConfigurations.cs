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
    public class StoreConfigurations : IEntityTypeConfiguration<Store>
    {
        public void Configure(EntityTypeBuilder<Store> builder)
        {
            builder.ToTable("Store");

            builder.HasKey(x => x.Id);


            builder.HasOne(x => x.Address)
                .WithOne(x => x.Store)
                .HasForeignKey<Store>(x => x.AddressId)
                .OnDelete(DeleteBehavior.Restrict);




        }

    }
}
