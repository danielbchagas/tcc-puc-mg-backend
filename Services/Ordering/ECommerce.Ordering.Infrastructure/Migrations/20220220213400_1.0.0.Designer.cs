﻿// <auto-generated />
using System;
using ECommerce.Ordering.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ECommerce.Ordering.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20220220213400_1.0.0")]
    partial class _100
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.10");

            modelBuilder.Entity("ECommerce.Ordering.Domain.Models.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Document")
                        .IsRequired()
                        .HasColumnType("varchar(18)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("FirstLine")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("varchar(150)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.Property<DateTime>("RegistrationDate")
                        .HasColumnType("datetime");

                    b.Property<string>("SecondLine")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("char(2)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("char(15)");

                    b.Property<decimal>("Value")
                        .HasColumnType("money");

                    b.Property<string>("ZipCode")
                        .IsRequired()
                        .HasColumnType("varchar(9)");

                    b.HasKey("Id");

                    b.ToTable("Ordering");
                });

            modelBuilder.Entity("ECommerce.Ordering.Domain.Models.OrderItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Image")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<decimal>("Value")
                        .HasColumnType("money");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("ECommerce.Ordering.Domain.Models.OrderItem", b =>
                {
                    b.HasOne("ECommerce.Ordering.Domain.Models.Order", "Order")
                        .WithMany("Items")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");
                });

            modelBuilder.Entity("ECommerce.Ordering.Domain.Models.Order", b =>
                {
                    b.Navigation("Items");
                });
#pragma warning restore 612, 618
        }
    }
}