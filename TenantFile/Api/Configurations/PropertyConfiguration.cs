using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using TenantFile.Api.Models;

namespace TenantFile.Api.Configurations
{
    public class PropertyConfiguration : IEntityTypeConfiguration<Property>
    {
        public void Configure(EntityTypeBuilder<Property> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasColumnName("PropertyId")
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder
              .HasMany(p => p.Residences)
              .WithOne(r => r.Property)
              .HasForeignKey(p => p.Id);
        }
    }
}
