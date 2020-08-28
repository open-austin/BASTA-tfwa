using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
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

            builder.Property(rr => rr.MoveIn)
                .HasConversion(
                    i => i.UtcDateTime,
                    u => u.ToLocalTime());
            
            builder.Property(rr => rr.MoveOut)
                .HasConversion(
                    i => i.UtcDateTime,
                    u => u.ToLocalTime());

            builder.Property(e => e.Id)
                  .HasColumnName("ResidenceRecordId")
                  .IsRequired()
                  .ValueGeneratedOnAdd();
        }
    }
}
