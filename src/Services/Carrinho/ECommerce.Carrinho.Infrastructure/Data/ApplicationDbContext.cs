using ECommerce.Carrinho.Domain.Interfaces.Data;
using ECommerce.Carrinho.Domain.Models;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ECommerce.Carrinho.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext, IUnitOfWork
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :base(options)
        {

        }

        public DbSet<ItemCarrinho> ItensCarrinhos { get; set; }
        public DbSet<Domain.Models.Carrinho> CarrinhosClientes { get; set; }

        public async Task<bool> Commit()
        {
            return await base.SaveChangesAsync() > 0;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<ValidationResult>();

            modelBuilder.Entity<ItemCarrinho>(ic => 
            {
                ic.ToTable("ItemCarrinho");

                ic.HasKey(ci => ci.Id);

                ic.Property(ic => ic.Imagem).HasColumnType("text").IsRequired(false);
                ic.Property(ic => ic.Nome).HasColumnType("varchar(20)").IsRequired();
                ic.Property(ic => ic.Valor).HasColumnType("money").IsRequired();
                ic.Property(ic => ic.Quantidade).HasColumnType("int").IsRequired();
            });

            modelBuilder.Entity<Domain.Models.Carrinho>(cc => 
            {
                cc.ToTable("Carrinho");

                cc.HasKey(cc => cc.Id);

                cc.HasIndex(cc => cc.ClienteId);

                cc.Property(cc => cc.ValorTotal).HasColumnType("money");

                cc.HasMany(cc => cc.Itens).WithOne(ic => ic.Carrinho).HasForeignKey(ic => ic.CarrinhoId);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
