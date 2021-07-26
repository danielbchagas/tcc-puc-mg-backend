using ECommerce.Clientes.Domain.Interfaces.Data;
using ECommerce.Clientes.Domain.Models;
using Microsoft.EntityFrameworkCore;
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
        public DbSet<LogEvento> LogEventos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (Debugger.IsAttached)
                optionsBuilder.LogTo(Console.WriteLine);

            // Para rodar migrações
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer("Server=localhost;Database=ClientesDB;User Id=sa;Password=yourStrong(!)Password;");

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            #region Seed
            var idCliente1 = Guid.NewGuid();
            var idCliente2 = Guid.NewGuid();
            var idCliente3 = Guid.NewGuid();
            var idCliente4 = Guid.NewGuid();
            var idCliente5 = Guid.NewGuid();

            var endereco1 = new Endereco();
            var endereco2 = new Endereco();
            var endereco3 = new Endereco();
            var endereco4 = new Endereco();
            var endereco5 = new Endereco();

            modelBuilder.Entity<Cliente>().HasData(
                new Cliente(id: idCliente1, nomeFantasia: "Farmácia Santa Lúcia", cnpj: "41.822.341/0001-20", endereco: endereco1),
                new Cliente(id: idCliente1, nomeFantasia: "Farmácia Pacheco", cnpj: "20.188.964/0001-12", endereco: endereco2),
                new Cliente(id: idCliente1, nomeFantasia: "Farmácia Mônica", cnpj: "55.142.194/0001-51", endereco: endereco3),
                new Cliente(id: idCliente1, nomeFantasia: "Farmácia Ultrafarma", cnpj: "29.614.369/0001-76", endereco: endereco4),
                new Cliente(id: idCliente1, nomeFantasia: "Farmácia Preço Baixo", cnpj: "77.749.871/0001-37", endereco: endereco5)
            );

            modelBuilder.Entity<LogEvento>().HasData(
                new LogEvento(id: Guid.NewGuid(), origemRequisicao: "ip", momento: new DateTime(2021, 01, 01), uri: "ip/clientes/novo", clienteId: idCliente1),
                new LogEvento(id: Guid.NewGuid(), origemRequisicao: "ip", momento: new DateTime(2021, 02, 01), uri: "ip/clientes/novo", clienteId: idCliente2),
                new LogEvento(id: Guid.NewGuid(), origemRequisicao: "ip", momento: new DateTime(2021, 03, 01), uri: "ip/clientes/novo", clienteId: idCliente3),
                new LogEvento(id: Guid.NewGuid(), origemRequisicao: "ip", momento: new DateTime(2021, 04, 01), uri: "ip/clientes/novo", clienteId: idCliente4),
                new LogEvento(id: Guid.NewGuid(), origemRequisicao: "ip", momento: new DateTime(2021, 05, 01), uri: "ip/clientes/novo", clienteId: idCliente5)
            );
            #endregion

            base.OnModelCreating(modelBuilder);
        }

        public async Task<bool> Commit()
        {
            return await base.SaveChangesAsync() > 0;
        }
    }
}
