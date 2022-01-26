﻿// <auto-generated />
using System;
using ECommerce.Customers.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ECommerce.Customers.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20220126113606_address-mapping")]
    partial class addressmapping
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.9");

            modelBuilder.Entity("ECommerce.Customers.Domain.Models.Address", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FirstLine")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<string>("SecondLine")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<int>("State")
                        .HasColumnType("char(2)");

                    b.Property<string>("ZipCode")
                        .IsRequired()
                        .HasColumnType("varchar(9)");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId")
                        .IsUnique();

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("ECommerce.Customers.Domain.Models.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<bool>("Enabled")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("ECommerce.Customers.Domain.Models.Document", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasColumnType("varchar(18)");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId")
                        .IsUnique();

                    b.ToTable("Documents");
                });

            modelBuilder.Entity("ECommerce.Customers.Domain.Models.Email", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId")
                        .IsUnique();

                    b.ToTable("Emails");
                });

            modelBuilder.Entity("ECommerce.Customers.Domain.Models.Phone", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId")
                        .IsUnique();

                    b.ToTable("Phones");
                });

            modelBuilder.Entity("ECommerce.Customers.Domain.Models.Address", b =>
                {
                    b.HasOne("ECommerce.Customers.Domain.Models.Customer", "Customer")
                        .WithOne("Address")
                        .HasForeignKey("ECommerce.Customers.Domain.Models.Address", "CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("ECommerce.Customers.Domain.Models.Document", b =>
                {
                    b.HasOne("ECommerce.Customers.Domain.Models.Customer", "Customer")
                        .WithOne("Document")
                        .HasForeignKey("ECommerce.Customers.Domain.Models.Document", "CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("ECommerce.Customers.Domain.Models.Email", b =>
                {
                    b.HasOne("ECommerce.Customers.Domain.Models.Customer", "Customer")
                        .WithOne("Email")
                        .HasForeignKey("ECommerce.Customers.Domain.Models.Email", "CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("ECommerce.Customers.Domain.Models.Phone", b =>
                {
                    b.HasOne("ECommerce.Customers.Domain.Models.Customer", "Customer")
                        .WithOne("Phone")
                        .HasForeignKey("ECommerce.Customers.Domain.Models.Phone", "CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("ECommerce.Customers.Domain.Models.Customer", b =>
                {
                    b.Navigation("Address");

                    b.Navigation("Document");

                    b.Navigation("Email");

                    b.Navigation("Phone");
                });
#pragma warning restore 612, 618
        }
    }
}
