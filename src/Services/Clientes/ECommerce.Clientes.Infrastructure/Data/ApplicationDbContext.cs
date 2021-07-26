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
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            #region Seed
            var cliente1 = new Cliente(id: Guid.NewGuid(), nomeFantasia: "Farmácia Santa Lúcia", cnpj: "41.822.341/0001-20");
            var cliente2 = new Cliente(id: Guid.NewGuid(), nomeFantasia: "Farmácia Pacheco", cnpj: "20.188.964/0001-12");
            var cliente3 = new Cliente(id: Guid.NewGuid(), nomeFantasia: "Farmácia Mônica", cnpj: "55.142.194/0001-51");
            var cliente4 = new Cliente(id: Guid.NewGuid(), nomeFantasia: "Farmácia Ultrafarma", cnpj: "29.614.369/0001-76");
            var cliente5 = new Cliente(id: Guid.NewGuid(), nomeFantasia: "Farmácia Preço Baixo", cnpj: "77.749.871/0001-37");

            var endereco1 = new Endereco(id: Guid.NewGuid(), logradouro: "Rua Santa Lúcia", bairro: "Cruz das Almas", cidade: "Maceió", cep: "57038-112", estado: Estados.AL);
            var endereco2 = new Endereco(id: Guid.NewGuid(), logradouro: "Avenida dos Imigrantes", bairro: "Panair", cidade: "Porto Velho", cep: "76801-400", estado: Estados.RO);
            var endereco3 = new Endereco(id: Guid.NewGuid(), logradouro: "Rua Praia da Redinha", bairro: "Nova Parnamirim", cidade: "Parnamirim", cep: "59151-415", estado: Estados.RN);
            var endereco4 = new Endereco(id: Guid.NewGuid(), logradouro: "Rua Campo do Brito", bairro: "São José", cidade: "Aracaju", cep: "49015-460", estado: Estados.SE);
            var endereco5 = new Endereco(id: Guid.NewGuid(), logradouro: "Rua El Greco", bairro: "Residencial Morumbi", cidade: "Goiânia", cep: "74574-009", estado: Estados.GO);

            var log1 = new LogEvento(id: Guid.NewGuid(), origemRequisicao: "ip", momento: new DateTime(2021, 01, 01), uri: "ip/clientes/novo", clienteId: cliente1.Id);
            var log2 = new LogEvento(id: Guid.NewGuid(), origemRequisicao: "ip", momento: new DateTime(2021, 02, 01), uri: "ip/clientes/novo", clienteId: cliente2.Id);
            var log3 = new LogEvento(id: Guid.NewGuid(), origemRequisicao: "ip", momento: new DateTime(2021, 03, 01), uri: "ip/clientes/novo", clienteId: cliente3.Id);
            var log4 = new LogEvento(id: Guid.NewGuid(), origemRequisicao: "ip", momento: new DateTime(2021, 04, 01), uri: "ip/clientes/novo", clienteId: cliente4.Id);
            var log5 = new LogEvento(id: Guid.NewGuid(), origemRequisicao: "ip", momento: new DateTime(2021, 05, 01), uri: "ip/clientes/novo", clienteId: cliente5.Id);

            
            #endregion

            base.OnModelCreating(modelBuilder);
        }

        public async Task<bool> Commit()
        {
            return await base.SaveChangesAsync() > 0;
        }
    }
}
