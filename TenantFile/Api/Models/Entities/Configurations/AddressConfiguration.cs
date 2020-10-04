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
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {           
            builder.HasKey(p => p.Id);

            builder.Property(e => e.Id)
                   .IsRequired()
                   .ValueGeneratedOnAdd();

            builder.Property(a => a.City)
                    .IsRequired();
            
            builder.Property(a => a.Street)
                    .IsRequired();
            
            builder.Property(a => a.StreetNumber)
                    .IsRequired();

            //Might like to make an enumeration class for this property to constrain options to valid values
            builder.Property(a => a.State)
                    .IsRequired();
                  //.HasMaxLength(2);
        }
    }
}