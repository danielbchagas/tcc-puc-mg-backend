﻿// <auto-generated />
using System;
using ECommerce.Produtos.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ECommerce.Produtos.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20210725235745_primeiro")]
    partial class primeiro
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.8")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ECommerce.Produtos.Domain.Models.LogEvento", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Momento")
                        .HasColumnType("date");

                    b.Property<string>("OrigemRequisicao")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("ProdutoId")
                        .IsRequired()
                        .HasColumnType("varchar(36)");

                    b.Property<string>("Uri")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.ToTable("LogEventos");

                    b.HasData(
                        new
                        {
                            Id = new Guid("354689b2-3263-4eb1-a73a-38bb1db7ed2d"),
                            Momento = new DateTime(2021, 7, 25, 20, 57, 45, 81, DateTimeKind.Local).AddTicks(2379),
                            OrigemRequisicao = "ip",
                            ProdutoId = "bc3f7b48-b624-463c-84e2-f29ead62282d",
                            Uri = "ip/produtos/novo"
                        },
                        new
                        {
                            Id = new Guid("b90da36c-3b6d-4c1d-8180-17af88c29bf8"),
                            Momento = new DateTime(2021, 7, 25, 20, 57, 45, 82, DateTimeKind.Local).AddTicks(4587),
                            OrigemRequisicao = "ip",
                            ProdutoId = "e34cb927-56e5-4ee6-abd9-4c4b79a621e4",
                            Uri = "ip/produtos/novo"
                        },
                        new
                        {
                            Id = new Guid("83018115-631a-42e8-81d5-5e49873feb9f"),
                            Momento = new DateTime(2021, 7, 25, 20, 57, 45, 82, DateTimeKind.Local).AddTicks(4607),
                            OrigemRequisicao = "ip",
                            ProdutoId = "ee9a8291-c868-482b-9727-cc802edcc42f",
                            Uri = "ip/produtos/novo"
                        },
                        new
                        {
                            Id = new Guid("67efba08-4d72-46fb-abb7-000307a50f31"),
                            Momento = new DateTime(2021, 7, 25, 20, 57, 45, 82, DateTimeKind.Local).AddTicks(4617),
                            OrigemRequisicao = "ip",
                            ProdutoId = "1d82fdc8-e55a-453f-baac-3f8a952c986d",
                            Uri = "ip/produtos/novo"
                        },
                        new
                        {
                            Id = new Guid("54ed2b84-6e7d-4f3e-a476-9ec5824c87ef"),
                            Momento = new DateTime(2021, 7, 25, 20, 57, 45, 82, DateTimeKind.Local).AddTicks(4619),
                            OrigemRequisicao = "ip",
                            ProdutoId = "c5d91675-4c65-42bd-a78b-9a1979be8a6d",
                            Uri = "ip/produtos/novo"
                        });
                });

            modelBuilder.Entity("ECommerce.Produtos.Domain.Models.Produto", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Fabricacao")
                        .HasColumnType("date");

                    b.Property<string>("Lote")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Marca")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Observacao")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<int>("Quantidade")
                        .HasColumnType("int");

                    b.Property<DateTime>("Vencimento")
                        .HasColumnType("date");

                    b.HasKey("Id");

                    b.ToTable("Produtos");

                    b.HasData(
                        new
                        {
                            Id = new Guid("bc3f7b48-b624-463c-84e2-f29ead62282d"),
                            Fabricacao = new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Lote = "xxx-5454",
                            Marca = "Pfizer",
                            Nome = "Vacina",
                            Observacao = "Vacina contra COVID-19.",
                            Quantidade = 1000000,
                            Vencimento = new DateTime(2021, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = new Guid("e34cb927-56e5-4ee6-abd9-4c4b79a621e4"),
                            Fabricacao = new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Lote = "xxx-5324",
                            Marca = "AstraZeneca",
                            Nome = "Vacina",
                            Observacao = "Vacina contra COVID-19.",
                            Quantidade = 500000,
                            Vencimento = new DateTime(2021, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = new Guid("ee9a8291-c868-482b-9727-cc802edcc42f"),
                            Fabricacao = new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Lote = "xxx-6654",
                            Marca = "Janssen",
                            Nome = "Vacina",
                            Observacao = "Vacina contra COVID-19.",
                            Quantidade = 1000000,
                            Vencimento = new DateTime(2021, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = new Guid("1d82fdc8-e55a-453f-baac-3f8a952c986d"),
                            Fabricacao = new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Lote = "xxx-1054",
                            Marca = "GlaxoSmithKline",
                            Nome = "Centrum",
                            Observacao = "Suplemento alimentar (multivitamínico).",
                            Quantidade = 1000000,
                            Vencimento = new DateTime(2021, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = new Guid("c5d91675-4c65-42bd-a78b-9a1979be8a6d"),
                            Fabricacao = new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Lote = "xxx-2154",
                            Marca = "Colgate",
                            Nome = "Enxaguante bucal",
                            Observacao = "Enxaguante bucal.",
                            Quantidade = 1000000,
                            Vencimento = new DateTime(2021, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });
#pragma warning restore 612, 618
        }
    }
}