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
    public class TenantConfiguration : IEntityTypeConfiguration<Tenant>
    {
        public void Configure(EntityTypeBuilder<Tenant> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id).HasColumnName("TenantId")
                     .IsRequired()
                     .ValueGeneratedOnAdd();

            builder.Property(e => e.Name).HasColumnName("TenantName")
                    .IsRequired();
        }
    }
}
