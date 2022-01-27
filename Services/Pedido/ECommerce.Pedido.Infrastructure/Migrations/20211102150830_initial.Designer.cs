﻿// <auto-generated />
using System;
using ECommerce.Ordering.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ECommerce.Pedido.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20211102150830_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.10");

            modelBuilder.Entity("ECommerce.Pedido.Domain.Models.Cliente", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Sobrenome")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Clientes");
                });

            modelBuilder.Entity("ECommerce.Pedido.Domain.Models.Documento", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ClienteId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Numero")
                        .IsRequired()
                        .HasColumnType("varchar(18)");

                    b.HasKey("Id");

                    b.HasIndex("ClienteId")
                        .IsUnique();

                    b.ToTable("Documentos");
                });

            modelBuilder.Entity("ECommerce.Pedido.Domain.Models.Email", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ClienteId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Endereco")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("ClienteId")
                        .IsUnique();

                    b.ToTable("Emails");
                });

            modelBuilder.Entity("ECommerce.Pedido.Domain.Models.Endereco", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Bairro")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Cep")
                        .IsRequired()
                        .HasColumnType("varchar(9)");

                    b.Property<string>("Cidade")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<Guid>("ClienteId")
                        .HasColumnType("TEXT");

                    b.Property<int>("Estado")
                        .HasColumnType("char(2)");

                    b.Property<string>("Logradouro")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.HasKey("Id");

                    b.HasIndex("ClienteId")
                        .IsUnique();

                    b.ToTable("Enderecos");
                });

            modelBuilder.Entity("ECommerce.Pedido.Domain.Models.Pedido", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("ClienteId")
                        .HasColumnType("TEXT");

                    b.Property<int>("Status")
                        .HasColumnType("char(15)");

                    b.Property<decimal>("Value")
                        .HasColumnType("money");

                    b.HasKey("Id");

                    b.HasIndex("ClienteId");

                    b.ToTable("Ordering");
                });

            modelBuilder.Entity("ECommerce.Pedido.Domain.Models.PedidoItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Imagem")
                        .HasColumnType("text");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<Guid?>("PedidoId")
                        .HasColumnType("TEXT");

                    b.Property<int>("Quantidade")
                        .HasColumnType("int");

                    b.Property<decimal>("Value")
                        .HasColumnType("money");

                    b.HasKey("Id");

                    b.HasIndex("PedidoId");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("ECommerce.Pedido.Domain.Models.Telefone", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ClienteId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Numero")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.HasKey("Id");

                    b.HasIndex("ClienteId")
                        .IsUnique();

                    b.ToTable("Telefones");
                });

            modelBuilder.Entity("ECommerce.Pedido.Domain.Models.Documento", b =>
                {
                    b.HasOne("ECommerce.Pedido.Domain.Models.Cliente", "Cliente")
                        .WithOne("Documento")
                        .HasForeignKey("ECommerce.Pedido.Domain.Models.Documento", "ClienteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cliente");
                });

            modelBuilder.Entity("ECommerce.Pedido.Domain.Models.Email", b =>
                {
                    b.HasOne("ECommerce.Pedido.Domain.Models.Cliente", "Cliente")
                        .WithOne("Email")
                        .HasForeignKey("ECommerce.Pedido.Domain.Models.Email", "ClienteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cliente");
                });

            modelBuilder.Entity("ECommerce.Pedido.Domain.Models.Endereco", b =>
                {
                    b.HasOne("ECommerce.Pedido.Domain.Models.Cliente", "Cliente")
                        .WithOne("Endereco")
                        .HasForeignKey("ECommerce.Pedido.Domain.Models.Endereco", "ClienteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cliente");
                });

            modelBuilder.Entity("ECommerce.Pedido.Domain.Models.Pedido", b =>
                {
                    b.HasOne("ECommerce.Pedido.Domain.Models.Cliente", "Cliente")
                        .WithMany()
                        .HasForeignKey("ClienteId");

                    b.Navigation("Cliente");
                });

            modelBuilder.Entity("ECommerce.Pedido.Domain.Models.PedidoItem", b =>
                {
                    b.HasOne("ECommerce.Pedido.Domain.Models.Pedido", null)
                        .WithMany("Items")
                        .HasForeignKey("PedidoId");
                });

            modelBuilder.Entity("ECommerce.Pedido.Domain.Models.Telefone", b =>
                {
                    b.HasOne("ECommerce.Pedido.Domain.Models.Cliente", "Cliente")
                        .WithOne("Telefone")
                        .HasForeignKey("ECommerce.Pedido.Domain.Models.Telefone", "ClienteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cliente");
                });

            modelBuilder.Entity("ECommerce.Pedido.Domain.Models.Cliente", b =>
                {
                    b.Navigation("Documento");

                    b.Navigation("Email");

                    b.Navigation("Endereco");

                    b.Navigation("Telefone");
                });

            modelBuilder.Entity("ECommerce.Pedido.Domain.Models.Pedido", b =>
                {
                    b.Navigation("Items");
                });
#pragma warning restore 612, 618
        }
    }
}
