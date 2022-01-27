using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using ECommerce.Carts.Domain.Interfaces.Data;
using ECommerce.Carts.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Carts.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext, IUnitOfWork
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :base(options) {}

        public DbSet<Item> Items { get; set; }
        public DbSet<Cart> Carts { get; set; }

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
