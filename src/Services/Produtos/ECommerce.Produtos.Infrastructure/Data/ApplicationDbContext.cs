using ECommerce.Common.Interfaces.Data;
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

            modelBuilder.Entity<Produto>().HasData(
                new Produto(id: Guid.NewGuid(), marca: "Pfizer", nome: "Vacina", lote: "xxx-5454", fabricacao: new DateTime(2021, 01, 01), vencimento: new DateTime(2021, 12, 31), observacao: "Vacina contra COVID-19.", quantidade: 1_000_000),
                new Produto(id: Guid.NewGuid(), marca: "AstraZeneca", nome: "Vacina", lote: "xxx-5324", fabricacao: new DateTime(2021, 01, 01), vencimento: new DateTime(2021, 12, 31), observacao: "Vacina contra COVID-19.", quantidade: 500_000),
                new Produto(id: Guid.NewGuid(), marca: "Janssen", nome: "Vacina", lote: "xxx-6654", fabricacao: new DateTime(2021, 01, 01), vencimento: new DateTime(2021, 12, 31), observacao: "Vacina contra COVID-19.", quantidade: 1_000_000)
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}
