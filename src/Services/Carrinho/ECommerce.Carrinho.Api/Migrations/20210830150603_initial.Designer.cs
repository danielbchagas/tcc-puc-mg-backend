﻿// <auto-generated />
using System;
using ECommerce.Carrinho.Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ECommerce.Carrinho.Api.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20210830150603_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.9");

            modelBuilder.Entity("ECommerce.Carrinho.Api.Models.Carrinho", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ClienteId")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("ValorTotal")
                        .HasColumnType("money");

                    b.HasKey("Id");

                    b.HasIndex("ClienteId");

                    b.ToTable("Carrinho");
                });

            modelBuilder.Entity("ECommerce.Carrinho.Api.Models.ItemCarrinho", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("CarrinhoId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Imagem")
                        .HasColumnType("text");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.Property<Guid>("ProdutoId")
                        .HasColumnType("TEXT");

                    b.Property<int>("Quantidade")
                        .HasColumnType("int");

                    b.Property<decimal>("Valor")
                        .HasColumnType("money");

                    b.HasKey("Id");

                    b.HasIndex("CarrinhoId");

                    b.ToTable("ItemCarrinho");
                });

            modelBuilder.Entity("ECommerce.Carrinho.Api.Models.ItemCarrinho", b =>
                {
                    b.HasOne("ECommerce.Carrinho.Api.Models.Carrinho", "CarrinhoCliente")
                        .WithMany("Itens")
                        .HasForeignKey("CarrinhoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CarrinhoCliente");
                });

            modelBuilder.Entity("ECommerce.Carrinho.Api.Models.Carrinho", b =>
                {
                    b.Navigation("Itens");
                });
#pragma warning restore 612, 618
        }
    }
}
