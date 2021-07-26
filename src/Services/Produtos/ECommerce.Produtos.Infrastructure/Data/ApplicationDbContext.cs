using ECommerce.Produtos.Domain.Interfaces.Data;
using ECommerce.Produtos.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ECommerce.Produtos.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext, IUnitOfWork
    {
        public ApplicationDbContext() { }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IMediator mediator) : base(options) 
        {
            _mediator = mediator;
        }

        private readonly IMediator _mediator;
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<LogEvento> LogEventos { get; set; }

        public async Task<bool> Commit()
        {
            return await base.SaveChangesAsync() > 0;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (Debugger.IsAttached)
                optionsBuilder.LogTo(Console.WriteLine);

            // Para rodar migrações
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer("Server=localhost;Database=ProdutosDB;User Id=sa;Password=yourStrong(!)Password;");

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            #region Seed
            var idProduto1 = Guid.NewGuid();
            var idProduto2 = Guid.NewGuid();
            var idProduto3 = Guid.NewGuid();
            var idProduto4 = Guid.NewGuid();
            var idProduto5 = Guid.NewGuid();

            modelBuilder.Entity<Produto>().HasData(
                new Produto(id: idProduto1, marca: "Pfizer", nome: "Vacina", lote: "xxx-5454", fabricacao: new DateTime(2021, 01, 01), vencimento: new DateTime(2021, 12, 31), observacao: "Vacina contra COVID-19.", quantidade: 1_000_000),
                new Produto(id: idProduto2, marca: "AstraZeneca", nome: "Vacina", lote: "xxx-5324", fabricacao: new DateTime(2021, 01, 01), vencimento: new DateTime(2021, 12, 31), observacao: "Vacina contra COVID-19.", quantidade: 500_000),
                new Produto(id: idProduto3, marca: "Janssen", nome: "Vacina", lote: "xxx-6654", fabricacao: new DateTime(2021, 01, 01), vencimento: new DateTime(2021, 12, 31), observacao: "Vacina contra COVID-19.", quantidade: 1_000_000),
                new Produto(id: idProduto4, marca: "GlaxoSmithKline", nome: "Centrum", lote: "xxx-1054", fabricacao: new DateTime(2021, 01, 01), vencimento: new DateTime(2021, 12, 31), observacao: "Suplemento alimentar (multivitamínico).", quantidade: 1_000_000),
                new Produto(id: idProduto5, marca: "Colgate", nome: "Enxaguante bucal", lote: "xxx-2154", fabricacao: new DateTime(2021, 01, 01), vencimento: new DateTime(2021, 12, 31), observacao: "Enxaguante bucal.", quantidade: 1_000_000)
            );

            modelBuilder.Entity<LogEvento>().HasData(
                new LogEvento(id: Guid.NewGuid(), origemRequisicao: "ip", momento: DateTime.Now, uri: "ip/produtos/novo", produtoId: idProduto1),
                new LogEvento(id: Guid.NewGuid(), origemRequisicao: "ip", momento: DateTime.Now, uri: "ip/produtos/novo", produtoId: idProduto2),
                new LogEvento(id: Guid.NewGuid(), origemRequisicao: "ip", momento: DateTime.Now, uri: "ip/produtos/novo", produtoId: idProduto3),
                new LogEvento(id: Guid.NewGuid(), origemRequisicao: "ip", momento: DateTime.Now, uri: "ip/produtos/novo", produtoId: idProduto4),
                new LogEvento(id: Guid.NewGuid(), origemRequisicao: "ip", momento: DateTime.Now, uri: "ip/produtos/novo", produtoId: idProduto5)
            );
            #endregion

            base.OnModelCreating(modelBuilder);
        }
    }
}
