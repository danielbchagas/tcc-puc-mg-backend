﻿using System;
using System.Diagnostics;
using System.Threading.Tasks;
using ECommerce.Cliente.Domain.Enums;
using ECommerce.Cliente.Domain.Interfaces.Data;
using ECommerce.Cliente.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ECommerce.Cliente.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext, IUnitOfWork
    {
        public ApplicationDbContext()
        {

        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        
        public DbSet<Domain.Models.Cliente> Clientes { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<LogEvento> LogEventos { get; set; }
        public DbSet<Documento> Documentos { get; set; }
        public DbSet<Email> Emails { get; set; }
        public DbSet<Telefone> Telefones { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (Debugger.IsAttached)
                optionsBuilder
                    .LogTo(Console.WriteLine, LogLevel.Error)
                    .EnableSensitiveDataLogging();

            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer("Server=localhost;Database=ClienteDB;User Id=sa;Password=yourStrong(!)Password;");

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Mapeamento
            modelBuilder.Entity<Domain.Models.Cliente>(c =>
            {
                c.ToTable("Clientes");

                c.HasKey(c => c.Id);

                c.Property(c => c.Nome).HasColumnType("varchar(50)").IsRequired();
                c.Property(c => c.Sobrenome).HasColumnType("varchar(100)").IsRequired();
                c.Property(c => c.DataNascimento).HasColumnType("date").IsRequired();
                c.Property(c => c.Ativo).HasColumnType("bit").IsRequired();

                c.HasOne(c => c.Documento)
                    .WithOne(d => d.Cliente)
                    .OnDelete(DeleteBehavior.Cascade);
                c.HasOne(c => c.Email)
                    .WithOne(e => e.Cliente)
                    .OnDelete(DeleteBehavior.Cascade);
                c.HasOne(c => c.Telefone)
                    .WithOne(t => t.Cliente)
                    .OnDelete(DeleteBehavior.Cascade);
                c.HasOne(c => c.Endereco)
                    .WithOne(e => e.Cliente)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Documento>(d =>
            {
                d.ToTable("Documentos");

                d.HasKey(d => d.Id);

                d.Property(d => d.Numero).HasColumnType("varchar(18)").IsRequired();
                d.Property(e => e.ClienteId).HasColumnType("uniqueidentifier").IsRequired();
            });

            modelBuilder.Entity<Email>(e =>
            {
                e.ToTable("Emails");

                e.HasKey(e => e.Id);

                e.Property(e => e.Endereco).HasColumnType("varchar(100)").IsRequired();
                e.Property(e => e.ClienteId).HasColumnType("uniqueidentifier").IsRequired();
            });

            modelBuilder.Entity<Telefone>(e =>
            {
                e.ToTable("Telefones");

                e.HasKey(e => e.Id);

                e.Property(e => e.Numero).HasColumnType("varchar(20)").IsRequired();
                e.Property(e => e.ClienteId).HasColumnType("uniqueidentifier").IsRequired();
            });

            modelBuilder.Entity<Endereco>(e => 
            {
                e.ToTable("Enderecos");

                e.HasKey(e => e.Id);

                e.Property(e => e.Logradouro).HasColumnType("varchar(200)").IsRequired();
                e.Property(e => e.Bairro).HasColumnType("varchar(50)").IsRequired();
                e.Property(e => e.Cidade).HasColumnType("varchar(50)").IsRequired();
                e.Property(e => e.Cep).HasColumnType("varchar(9)").IsRequired();
                e.Property(e => e.Estado).HasColumnType("char(2)").IsRequired();
                e.Property(e => e.ClienteId).HasColumnType("uniqueidentifier").IsRequired();
            });

            modelBuilder.Entity<LogEvento>(le => 
            {
                le.ToTable("LogEventos");

                le.HasKey(le => le.Id);

                le.Property(le => le.Momento).HasColumnType("date").IsRequired();
                le.Property(l => l.EntidadeId).HasColumnType("uniqueidentifier").IsRequired();
                le.Property(l => l.UsuarioId).HasColumnType("uniqueidentifier").IsRequired();
            });
            #endregion

            Seed(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        public async Task<bool> Commit()
        {
            return await base.SaveChangesAsync() > 0;
        }

        private void Seed(ModelBuilder modelBuilder)
        {
            #region Cliente 1
            var cliente1 = new Domain.Models.Cliente(
                nome: "Davi Giovanni Felipe",
                sobrenome: "Fernandes",
                dataNascimento: new DateTime(1955, 02, 07)
            );
            var documentoCliente1 = new Documento(
                numero: "903.142.734-92",
                clienteId: cliente1.Id
            );
            var emailCliente1 = new Email(
                endereco: "davi_giovanni_felipe@gmail.com",
                cliente1.Id
            );
            var telefoneCliente1 = new Telefone(
                numero: "(82) 98621-8773",
                clienteId: cliente1.Id
            );
            var enderecoCliente1 = new Endereco(
                logradouro: "Colônia Agrícola Águas Claras Chácara 23, 641",
                bairro: "Guará I",
                cidade: "Brasília",
                cep: "71090-265",
                estado: Estados.DF,
                clienteId: cliente1.Id
            );
            #endregion
            
            #region Cliente 2
            var cliente2 = new Domain.Models.Cliente(
                nome: "Ayla Caroline",
                sobrenome: "Ana Gomes",
                dataNascimento: new DateTime(1963, 12, 12)
            );
            var documentoCliente2 = new Documento(
                numero: "668.154.787-77",
                clienteId: cliente2.Id
            );
            var emailCliente2 = new Email(
                endereco: "ayla_caroline_ana_gomes@gmail.com",
                cliente2.Id
            );
            var telefoneCliente2 = new Telefone(
                numero: "(91) 98965-5955",
                clienteId: cliente2.Id
            );
            var enderecoCliente2 = new Endereco(
                logradouro: "Praça São Francisco de Assis, 442",
                bairro: "Tarumã",
                cidade: "Curitiba",
                cep: "82530-220",
                estado: Estados.PR,
                clienteId: cliente2.Id
            );
            #endregion

            #region Cliente 3
            var cliente3 = new Domain.Models.Cliente(
                nome: "BetinaFlávia",
                sobrenome: "Souza",
                dataNascimento: new DateTime(1975, 02, 16)
            );
            var documentoCliente3 = new Documento(
                numero: "345.712.047-10",
                clienteId: cliente3.Id
            );
            var emailCliente3 = new Email(
                endereco: "b_etina_flavia_souza@gmail.com",
                cliente3.Id
            );
            var telefoneCliente3 = new Telefone(
                numero: "(95) 98234-7636",
                clienteId: cliente3.Id
            );
            var enderecoCliente3 = new Endereco(
                logradouro: "Rua Neves, 378",
                bairro: "Abegay",
                cidade: "Cruz Alta",
                cep: "98045-115",
                estado: Estados.RS,
                clienteId: cliente3.Id
            );
            #endregion
            
            modelBuilder.Entity<Domain.Models.Cliente>().HasData(
                cliente1,
                cliente2,
                cliente3
            );

            modelBuilder.Entity<Documento>().HasData(
                documentoCliente1,
                documentoCliente2,
                documentoCliente3
            );

            modelBuilder.Entity<Email>().HasData(
                emailCliente1,
                emailCliente2,
                emailCliente3
            );

            modelBuilder.Entity<Telefone>().HasData(
                telefoneCliente1,
                telefoneCliente2,
                telefoneCliente3
            );

            modelBuilder.Entity<Endereco>().HasData(
                enderecoCliente1,
                enderecoCliente2,
                enderecoCliente3
            );
        }
    }
}
