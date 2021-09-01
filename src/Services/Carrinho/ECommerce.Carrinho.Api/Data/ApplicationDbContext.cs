﻿using ECommerce.Carrinho.Api.Interfaces.Data;
using ECommerce.Carrinho.Api.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ECommerce.Carrinho.Api.Data
{
    public class ApplicationDbContext : DbContext, IUnitOfWork
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :base(options)
        {

        }

        public DbSet<ItemCarrinho> ItensCarrinhos { get; set; }
        public DbSet<Models.Carrinho> CarrinhosClientes { get; set; }

        public async Task<bool> Commit()
        {
            return await base.SaveChangesAsync() > 0;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ItemCarrinho>(ic => 
            {
                ic.ToTable("ItemCarrinho");

                ic.HasKey(ci => ci.Id);

                ic.Property(ic => ic.Imagem).HasColumnType("text").IsRequired(false);
                ic.Property(ic => ic.Nome).HasColumnType("varchar(20)").IsRequired();
                ic.Property(ic => ic.Valor).HasColumnType("money").IsRequired();
                ic.Property(ic => ic.Quantidade).HasColumnType("int").IsRequired();
            });

            modelBuilder.Entity<Models.Carrinho>(cc => 
            {
                cc.ToTable("Carrinho");

                cc.HasKey(cc => cc.Id);

                cc.HasIndex(cc => cc.ClienteId);

                cc.Property(cc => cc.ValorTotal).HasColumnType("money");

                cc.HasMany(cc => cc.Itens).WithOne(ic => ic.CarrinhoCliente).HasForeignKey(ic => ic.CarrinhoId);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
