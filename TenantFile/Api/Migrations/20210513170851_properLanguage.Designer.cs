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
    [DbContext(typeof(TenantFileContext))]
    [Migration("20210513170851_properLanguage")]
    partial class properLanguage
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.3")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("PhoneTenant", b =>
                {
                    b.Property<int>("PhonesId")
                        .HasColumnType("integer");

                    b.Property<int>("TenantsId")
                        .HasColumnType("integer");

                    b.HasKey("PhonesId", "TenantsId");

                    b.HasIndex("TenantsId");

                    b.ToTable("PhoneTenant");
                });

            modelBuilder.Entity("TenantFile.Api.Models.Entities.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Line1")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Line2")
                        .HasColumnType("text");

                    b.Property<string>("Line3")
                        .HasColumnType("text");

                    b.Property<string>("Line4")
                        .HasColumnType("text");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("TenantFile.Api.Models.Entities.Image", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("PhoneId")
                        .HasColumnType("integer");

                    b.Property<string>("ThumbnailName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("PhoneId");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("TenantFile.Api.Models.Entities.Organizer", b =>
                {
                    b.Property<string>("Uid")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Uid");

                    b.ToTable("Organizers");
                });

            modelBuilder.Entity("TenantFile.Api.Models.Entities.Phone", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("OrganizerUid")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("PreferredLanguage")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("OrganizerUid");

                    b.ToTable("Phones");
                });

            modelBuilder.Entity("TenantFile.Api.Models.Entities.Property", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("AddressId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("OrganizerUid")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.HasIndex("OrganizerUid");

                    b.ToTable("Properties");
                });

            modelBuilder.Entity("TenantFile.Api.Models.Entities.Residence", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("AddressId")
                        .HasColumnType("integer");

                    b.Property<int?>("PropertyId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.HasIndex("PropertyId");

                    b.ToTable("Residences");
                });

            modelBuilder.Entity("TenantFile.Api.Models.Entities.ResidenceRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("MoveIn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("MoveOut")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int?>("ResidenceId")
                        .HasColumnType("integer");

                    b.Property<int?>("TenantId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ResidenceId");

                    b.HasIndex("TenantId");

                    b.ToTable("ResidenceRecords");
                });

            modelBuilder.Entity("TenantFile.Api.Models.Entities.Tenant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("ResidenceId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ResidenceId");

                    b.ToTable("Tenants");
                });

            modelBuilder.Entity("PhoneTenant", b =>
                {
                    b.HasOne("TenantFile.Api.Models.Entities.Phone", null)
                        .WithMany()
                        .HasForeignKey("PhonesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TenantFile.Api.Models.Entities.Tenant", null)
                        .WithMany()
                        .HasForeignKey("TenantsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TenantFile.Api.Models.Entities.Image", b =>
                {
                    b.HasOne("TenantFile.Api.Models.Entities.Phone", null)
                        .WithMany("Images")
                        .HasForeignKey("PhoneId");

                    b.OwnsMany("TenantFile.Api.Models.Entities.ImageLabel", "Labels", b1 =>
                        {
                            b1.Property<int>("ImageId")
                                .HasColumnType("integer");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("integer")
                                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                            b1.Property<float?>("Confidence")
                                .HasColumnType("real");

                            b1.Property<string>("Label")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<string>("Source")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.HasKey("ImageId", "Id");

                            b1.ToTable("ImageLabel");

                            b1.WithOwner()
                                .HasForeignKey("ImageId");
                        });

                    b.Navigation("Labels");
                });

            modelBuilder.Entity("TenantFile.Api.Models.Entities.Phone", b =>
                {
                    b.HasOne("TenantFile.Api.Models.Entities.Organizer", null)
                        .WithMany("Phones")
                        .HasForeignKey("OrganizerUid");
                });

            modelBuilder.Entity("TenantFile.Api.Models.Entities.Property", b =>
                {
                    b.HasOne("TenantFile.Api.Models.Entities.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TenantFile.Api.Models.Entities.Organizer", null)
                        .WithMany("Properties")
                        .HasForeignKey("OrganizerUid");

                    b.Navigation("Address");
                });

            modelBuilder.Entity("TenantFile.Api.Models.Entities.Residence", b =>
                {
                    b.HasOne("TenantFile.Api.Models.Entities.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TenantFile.Api.Models.Entities.Property", "Property")
                        .WithMany("Residences")
                        .HasForeignKey("PropertyId");

                    b.Navigation("Address");

                    b.Navigation("Property");
                });

            modelBuilder.Entity("TenantFile.Api.Models.Entities.ResidenceRecord", b =>
                {
                    b.HasOne("TenantFile.Api.Models.Entities.Residence", "Residence")
                        .WithMany()
                        .HasForeignKey("ResidenceId");

                    b.HasOne("TenantFile.Api.Models.Entities.Tenant", "Tenant")
                        .WithMany()
                        .HasForeignKey("TenantId");

                    b.Navigation("Residence");

                    b.Navigation("Tenant");
                });

            modelBuilder.Entity("TenantFile.Api.Models.Entities.Tenant", b =>
                {
                    b.HasOne("TenantFile.Api.Models.Entities.Residence", "CurrentResidence")
                        .WithMany()
                        .HasForeignKey("ResidenceId");

                    b.Navigation("CurrentResidence");
                });

            modelBuilder.Entity("TenantFile.Api.Models.Entities.Organizer", b =>
                {
                    b.Navigation("Phones");

                    b.Navigation("Properties");
                });

            modelBuilder.Entity("TenantFile.Api.Models.Entities.Phone", b =>
                {
                    b.Navigation("Images");
                });

            modelBuilder.Entity("TenantFile.Api.Models.Entities.Property", b =>
                {
                    b.Navigation("Residences");
                });
#pragma warning restore 612, 618
        }
    }
}
