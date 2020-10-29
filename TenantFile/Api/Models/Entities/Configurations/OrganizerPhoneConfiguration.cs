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
    public class OrganizerPhoneConfiguration : IEntityTypeConfiguration<OrganizerPhone>
    {
        public void Configure(EntityTypeBuilder<OrganizerPhone> builder)
        {
            builder
                .HasKey(op => new { op.OrganizerId, op.PhoneId });

            builder
                .HasOne(op => op.Organizer)
                .WithMany(o => o.OrganizerPhones)
                .HasForeignKey(op => op.OrganizerId);

            builder
                .HasOne(op => op.Phone)
                .WithMany(p => p.OrganizerPhones)
                .HasForeignKey(op => op.PhoneId);
        }
    }
}