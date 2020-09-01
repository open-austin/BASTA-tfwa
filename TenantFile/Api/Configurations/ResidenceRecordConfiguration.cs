using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenantFile.Api.Models;

namespace TenantFile.Api.Configurations
{
    public class ResidenceRecordConfiguration : IEntityTypeConfiguration<ResidenceRecord>
    {
        public void Configure(EntityTypeBuilder<ResidenceRecord> builder)
        {
            builder.HasKey(p => p.Id);

           // var localDateConverter =
           //new ValueConverter<ZonedDateTime, DateTimeOffset>(v =>
           //    v.ToDateTimeOffset(),
           //    v => ZonedDateTime.FromDateTimeOffset(v));

           // builder.Property(e => e.MoveIn)
           //     .HasConversion(localDateConverter);
           // builder.Property(e => e.MoveOut)
           //     .HasConversion(localDateConverter);

            builder.Property(e => e.Id)
                   .IsRequired()
                  .ValueGeneratedOnAdd();
        }
    }
}
