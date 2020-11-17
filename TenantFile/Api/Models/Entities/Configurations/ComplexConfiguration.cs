using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TenantFile.Api.Models.Entities.Configurations
{
    public class ComplexConfiguration : IEntityTypeConfiguration<Complex>
    {
        public void Configure(EntityTypeBuilder<Complex> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(e => e.Id)
                   .IsRequired()
                   .ValueGeneratedOnAdd();

            //builder.HasMany(c => c.Properties)
            //    .WithOne(prop => prop.Complex)
            //    .HasForeignKey(prop => prop.Id)
            //    .IsRequired(false);
        }
    }
}