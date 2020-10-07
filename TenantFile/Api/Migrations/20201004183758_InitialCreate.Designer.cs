﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TenantFile.Api.Models;

namespace TenantFile.Api.Migrations
{
    [DbContext(typeof(TenantContext))]
    [Migration("20201004183758_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("TenantFile.Api.Models.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnName("city")
                        .HasColumnType("text");

                    b.Property<int>("PostalCode")
                        .HasColumnName("postal_code")
                        .HasColumnType("integer");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnName("state")
                        .HasColumnType("text");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnName("street")
                        .HasColumnType("text");

                    b.Property<string>("StreetNumber")
                        .IsRequired()
                        .HasColumnName("street_number")
                        .HasColumnType("text");

                    b.HasKey("Id")
                        .HasName("pk_addresses");

                    b.ToTable("addresses");
                });

            modelBuilder.Entity("TenantFile.Api.Models.Image", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("text");

                    b.Property<int?>("PhoneId")
                        .HasColumnName("phone_id")
                        .HasColumnType("integer");

                    b.Property<string>("ThumbnailName")
                        .IsRequired()
                        .HasColumnName("thumbnail_name")
                        .HasColumnType("text");

                    b.HasKey("Id")
                        .HasName("pk_images");

                    b.HasIndex("PhoneId")
                        .HasName("ix_images_phone_id");

                    b.ToTable("images");
                });

            modelBuilder.Entity("TenantFile.Api.Models.Phone", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnName("phone_number")
                        .HasColumnType("text");

                    b.HasKey("Id")
                        .HasName("pk_phones");

                    b.ToTable("phones");
                });

            modelBuilder.Entity("TenantFile.Api.Models.Property", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("AddressId")
                        .HasColumnName("address_id")
                        .HasColumnType("integer");

                    b.Property<string>("UnitIdentifier")
                        .IsRequired()
                        .HasColumnName("unit_identifier")
                        .HasColumnType("text");

                    b.HasKey("Id")
                        .HasName("pk_properties");

                    b.HasIndex("AddressId")
                        .HasName("ix_properties_address_id");

                    b.ToTable("properties");
                });

            modelBuilder.Entity("TenantFile.Api.Models.Residence", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("UnitIdentifier")
                        .IsRequired()
                        .HasColumnName("unit_identifier")
                        .HasColumnType("text");

                    b.HasKey("Id")
                        .HasName("pk_residences");

                    b.ToTable("residences");
                });

            modelBuilder.Entity("TenantFile.Api.Models.ResidenceRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("MoveIn")
                        .HasColumnName("move_in")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("MoveOut")
                        .HasColumnName("move_out")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("TenantId")
                        .HasColumnName("tenant_id")
                        .HasColumnType("integer");

                    b.HasKey("Id")
                        .HasName("pk_residence_records");

                    b.HasIndex("TenantId")
                        .HasName("ix_residence_records_tenant_id");

                    b.ToTable("residence_records");
                });

            modelBuilder.Entity("TenantFile.Api.Models.Tenant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("text");

                    b.HasKey("Id")
                        .HasName("pk_tenants");

                    b.ToTable("tenants");
                });

            modelBuilder.Entity("TenantFile.Api.Models.TenantPhone", b =>
                {
                    b.Property<int>("TenantId")
                        .HasColumnName("tenant_id")
                        .HasColumnType("integer");

                    b.Property<int>("PhoneId")
                        .HasColumnName("phone_id")
                        .HasColumnType("integer");

                    b.HasKey("TenantId", "PhoneId")
                        .HasName("pk_tenant_phone");

                    b.HasIndex("PhoneId")
                        .HasName("ix_tenant_phone_phone_id");

                    b.ToTable("tenant_phone");
                });

            modelBuilder.Entity("TenantFile.Api.Models.Image", b =>
                {
                    b.HasOne("TenantFile.Api.Models.Phone", null)
                        .WithMany("Images")
                        .HasForeignKey("PhoneId")
                        .HasConstraintName("fk_images_phones_phone_id");
                });

            modelBuilder.Entity("TenantFile.Api.Models.Property", b =>
                {
                    b.HasOne("TenantFile.Api.Models.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId")
                        .HasConstraintName("fk_properties_addresses_address_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TenantFile.Api.Models.Residence", b =>
                {
                    b.HasOne("TenantFile.Api.Models.Property", "Property")
                        .WithMany("Residences")
                        .HasForeignKey("Id")
                        .HasConstraintName("fk_residences_properties_property_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TenantFile.Api.Models.ResidenceRecord", b =>
                {
                    b.HasOne("TenantFile.Api.Models.Residence", "Residence")
                        .WithMany("ResidenceRecords")
                        .HasForeignKey("Id")
                        .HasConstraintName("fk_residence_records_residences_residence_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TenantFile.Api.Models.Tenant", "Tenant")
                        .WithMany("ResidenceRecords")
                        .HasForeignKey("TenantId")
                        .HasConstraintName("fk_residence_records_tenants_tenant_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TenantFile.Api.Models.TenantPhone", b =>
                {
                    b.HasOne("TenantFile.Api.Models.Phone", "Phone")
                        .WithMany("TenantPhones")
                        .HasForeignKey("PhoneId")
                        .HasConstraintName("fk_tenant_phone_phones_phone_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TenantFile.Api.Models.Tenant", "Tenant")
                        .WithMany("TenantPhones")
                        .HasForeignKey("TenantId")
                        .HasConstraintName("fk_tenant_phone_tenants_tenant_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}