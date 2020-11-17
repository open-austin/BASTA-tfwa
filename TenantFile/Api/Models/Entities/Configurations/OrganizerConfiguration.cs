using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenantFile.Api.Models.Entities;

namespace TenantFile.Api.Configurations
{
    public class OrganizerConfiguration : IEntityTypeConfiguration<Organizer>
    {
        public void Configure(EntityTypeBuilder<Organizer> builder)
        {
            builder.HasKey(e => e.Uid);

            builder.Property(e => e.Uid)
                     .IsRequired();
                     //.ValueGeneratedOnAdd();//this will be handled by the cloud function, uid converts to string

            builder.Property(e => e.Name)
                    .IsRequired();
        }
    }
}
