using ECommerce.Clientes.Domain.Interfaces.Data;
using ECommerce.Clientes.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ECommerce.Clientes.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext, IUnitOfWork
    {
        public ApplicationDbContext()
        {

        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<LogEvento> LogEventos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (Debugger.IsAttached)
                optionsBuilder
                    .LogTo(Console.WriteLine, LogLevel.Error)
                    .EnableSensitiveDataLogging();

            // Para rodar migrações
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer("Server=localhost;Database=ClientesDB;User Id=sa;Password=yourStrong(!)Password;");

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            #region Mapeamento
            modelBuilder.Entity<Cliente>(c =>
            {
                c.ToTable("Clientes");

                c.HasKey(c => c.Id);

                c.Property(c => c.NomeFantasia).HasColumnType("varchar(100)").IsRequired();
                c.Property(c => c.Ativo).HasColumnType("bit").IsRequired();

                c.HasOne(c => c.Documento).WithOne(d => d.Cliente);
                c.HasOne(c => c.Endereco).WithOne(d => d.Cliente);
            });

            modelBuilder.Entity<Documento>(d =>
            {
                d.ToTable("Documentos");

                d.HasKey(d => d.Id);

                d.Property(d => d.Numero).HasColumnType("varchar(18)").IsRequired();
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

            modelBuilder.Entity<LogEvento>(le => 
            {
                le.ToTable("LogEventos");

                le.HasKey(le => le.Id);

                le.Property(le => le.Momento).HasColumnType("date").IsRequired();
                le.Property(l => l.ClienteId).HasColumnType("varchar(36)").IsRequired();
                le.Property(l => l.UsuarioId).HasColumnType("varchar(36)").IsRequired();
            });
            #endregion

            base.OnModelCreating(modelBuilder);
        }

        public async Task<bool> Commit()
        {
            return await base.SaveChangesAsync() > 0;
        }
    }
}
