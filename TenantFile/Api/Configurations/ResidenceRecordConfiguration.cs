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
            //for postges naming conventions. EF automatically creates tables in PascalCase, Pg folds to lowercase so quotes would be needed with EF conventions 
            //builder.ToTable("residence_records");

            builder.HasKey(p => p.Id);

            builder.Property(e => e.Id)
                   .IsRequired()
                  .ValueGeneratedOnAdd();
        }
    }
}
