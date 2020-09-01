﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenantFile.Api.Models;

namespace TenantFile.Api.Configurations
{
    public class ResidenceConfiguration : IEntityTypeConfiguration<Residence>
    {
        public void Configure(EntityTypeBuilder<Residence> builder)
        {
            //for postges naming conventions. EF automatically creates tables in PascalCase, Pg folds to lowercase so quotes would be needed with EF conventions 
            //builder.ToTable("residences");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                     .IsRequired()
                     .ValueGeneratedOnAdd();
            builder
               .HasMany(r => r.ResidenceRecords)
               .WithOne(rr => rr.Residence)
               .HasForeignKey(rr => rr.Id);
        }
    }
}
