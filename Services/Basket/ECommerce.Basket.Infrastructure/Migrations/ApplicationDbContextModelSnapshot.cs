﻿// <auto-generated />
using System;
using ECommerce.Basket.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ECommerce.Basket.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.9");

            modelBuilder.Entity("ECommerce.Basket.Domain.Models.BasketItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("CustomerBasketId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Image")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<decimal>("Value")
                        .HasColumnType("money");

                    b.HasKey("Id");

                    b.HasIndex("CustomerBasketId");

                    b.ToTable("BasketItems");
                });

            modelBuilder.Entity("ECommerce.Basket.Domain.Models.CustomerBasket", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Value")
                        .HasColumnType("money");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Baskets");
                });

            modelBuilder.Entity("ECommerce.Basket.Domain.Models.BasketItem", b =>
                {
                    b.HasOne("ECommerce.Basket.Domain.Models.CustomerBasket", "CustomerBasket")
                        .WithMany("Items")
                        .HasForeignKey("CustomerBasketId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CustomerBasket");
                });

            modelBuilder.Entity("ECommerce.Basket.Domain.Models.CustomerBasket", b =>
                {
                    b.Navigation("Items");
                });
#pragma warning restore 612, 618
        }
    }
}
