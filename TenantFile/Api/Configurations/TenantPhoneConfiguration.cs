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
    public class TenantPhoneConfiguration : IEntityTypeConfiguration<TenantPhone>
    {
        public void Configure(EntityTypeBuilder<TenantPhone> builder)
        {
            builder
                .HasKey(tp => new { tp.TenantId, tp.PhoneId });

            builder.Property(p => p.PhoneId).HasColumnName("phone_id");
            
            builder.Property(t => t.TenantId).HasColumnName("tenant_id");

            builder
                .HasOne(tp => tp.Tenant)
                .WithMany(t => t.TenantPhones)
                .HasForeignKey(t => t.TenantId);

            builder
                .HasOne(tp => tp.Phone)
                .WithMany(p => p.TenantPhones)
                .HasForeignKey(tp => tp.PhoneId);
        }
    }
}