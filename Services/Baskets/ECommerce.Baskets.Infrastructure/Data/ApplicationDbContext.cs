using ECommerce.Baskets.Domain.Interfaces.Data;
using ECommerce.Baskets.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace ECommerce.Baskets.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext, IUnitOfWork
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Domain.Models.Basket> Baskets { get; set; }
        public DbSet<Item> Items { get; set; }

        public async Task<bool> Commit()
        {
            return await base.SaveChangesAsync() > 0;
        }

        public async Task<IDbContextTransaction> OpenTransaction()
        {
            return await base.Database.BeginTransactionAsync();
        }

        public async Task CommitTransaction()
        {
            await base.Database.CommitTransactionAsync();
        }

        public async Task RollbackTransaction()
        {
            await base.Database.RollbackTransactionAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Basket");

            modelBuilder.Ignore<ValidationResult>();

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
