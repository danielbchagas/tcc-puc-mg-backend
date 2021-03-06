using System.Threading.Tasks;
using ECommerce.Ordering.Domain.Interfaces.Data;
using ECommerce.Ordering.Domain.Models;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Ordering.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext, IUnitOfWork
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}

        public DbSet<Order> Ordering { get; set; }
        public DbSet<OrderItem> Items { get; set; }

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
