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
    public class PhoneConfiguration : IEntityTypeConfiguration<Phone>
    {
        public void Configure(EntityTypeBuilder<Phone> builder)
        {
            //for postges naming conventions. EF automatically creates tables in PascalCase, Pg folds to lowercase so quotes would be needed with EF conventions 
            //builder.ToTable("phone_numbers");

            builder.HasKey(e => e.Id);
            

            builder.Property(e => e.Id)
                     .IsRequired()
                     .ValueGeneratedOnAdd();

        }
    }
}