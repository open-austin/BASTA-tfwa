using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using TenantFile.Api.Models;
using TenantFile.Api.Models.Entities;

namespace TenantFile.Api.Configurations
{
    public class PropertyConfiguration : IEntityTypeConfiguration<Property>
    {
        public void Configure(EntityTypeBuilder<Property> builder)
        {
           
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder
              .HasMany(p => p.Residences)
              .WithOne()
              .HasForeignKey(r => r.PropertyId);

            //builder.HasOne(p=>p.Complex)
            //    .WithMany(collection=>collection.Properties)
        }
    }
}
