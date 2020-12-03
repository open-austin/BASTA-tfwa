using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenantFile.Api.Models;
using TenantFile.Api.Models.Entities;

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
            
            builder.Property(a => a.Line1)
                    .IsRequired();

            builder.Ignore(a => a.ValidationMessage);

            //Might like to make an enumeration class for this property to constrain options to valid values
            builder.Property(a => a.State)
                    .IsRequired();
                  //.HasMaxLength(2);
        }
    }
}