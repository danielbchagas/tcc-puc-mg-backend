using ECommerce.Pedido.Domain.Interfaces.Data;
using ECommerce.Pedido.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using PedidoCliente = ECommerce.Pedido.Domain.Models.Pedido;

namespace ECommerce.Pedido.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext, IUnitOfWork
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}

        #region Propriedades
        public DbSet<PedidoCliente> Pedidos { get; set; }
        public DbSet<PedidoItem> Itens { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Documento> Documentos { get; set; }
        public DbSet<Email> Emails { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Telefone> Telefones { get; set; }
        #endregion

        #region Métodos
        public async Task<bool> Commit()
        {
            return await base.SaveChangesAsync() > 0;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            #region Mapeamentos
            modelBuilder.Entity<PedidoCliente>(p => 
            {
                p.ToTable("Pedidos");

                p.HasKey(c => c.Id);

                p.Property(c => c.Valor).HasColumnType("money").IsRequired();
                p.Property(c => c.Status).HasColumnType("char(15)").IsRequired();

                p.HasOne(c => c.Cliente);
                p.HasMany(c => c.Itens);
            });

            modelBuilder.Entity<Cliente>(c =>
            {
                c.ToTable("Clientes");

                c.HasKey(c => c.Id);

                c.Property(c => c.Nome).HasColumnType("varchar(50)").IsRequired();
                c.Property(c => c.Sobrenome).HasColumnType("varchar(100)").IsRequired();
                
                c.HasOne(c => c.Documento);
                c.HasOne(c => c.Email);
                c.HasOne(c => c.Telefone);
                c.HasOne(c => c.Endereco);
            });

            modelBuilder.Entity<Documento>(d =>
            {
                d.ToTable("Documentos");

                d.HasKey(d => d.Id);

                d.Property(d => d.Numero).HasColumnType("varchar(18)").IsRequired();
            });

            modelBuilder.Entity<Email>(e =>
            {
                e.ToTable("Emails");

                e.HasKey(e => e.Id);

                e.Property(e => e.Endereco).HasColumnType("varchar(100)").IsRequired();
            });

            modelBuilder.Entity<Telefone>(e =>
            {
                e.ToTable("Telefones");

                e.HasKey(e => e.Id);

                e.Property(e => e.Numero).HasColumnType("varchar(20)").IsRequired();
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
            });

            modelBuilder.Entity<PedidoItem>(p =>
            {
                p.ToTable("Itens");

                p.HasKey(p => p.Id);

                p.Property(p => p.Nome).HasColumnType("varchar(50)").IsRequired();
                p.Property(p => p.Quantidade).HasColumnType("int").IsRequired();
                p.Property(p => p.Imagem).HasColumnType("text").IsRequired(false);
                p.Property(p => p.Valor).HasColumnType("money").IsRequired();
            });
            #endregion

            base.OnModelCreating(modelBuilder);
        }
        #endregion
    }
}
