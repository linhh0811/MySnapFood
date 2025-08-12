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
    public class OtpConfirmConfiguration : IEntityTypeConfiguration<OtpConfirm>
    {
        public void Configure(EntityTypeBuilder<OtpConfirm> builder)
        {
            builder.ToTable("OtpConfirms");

            builder.HasKey(x => x.Id);

        }
    }
}
