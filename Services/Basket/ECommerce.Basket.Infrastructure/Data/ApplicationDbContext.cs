using ECommerce.Basket.Domain.Interfaces.Data;
using ECommerce.Basket.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace ECommerce.Basket.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext, IUnitOfWork
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :base(options) {}

        public DbSet<ShoppingBasket> ShoppingBaskets { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }
        
        public async Task<bool> Commit()
        {
            return await base.SaveChangesAsync() > 0;
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<ValidationResult>();

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
