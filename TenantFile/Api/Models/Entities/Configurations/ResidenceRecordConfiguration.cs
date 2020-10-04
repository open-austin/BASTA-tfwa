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

            builder.Property(e => e.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();
        }
    }
}
