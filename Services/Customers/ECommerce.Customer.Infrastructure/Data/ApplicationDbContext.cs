using ECommerce.Customers.Domain.Interfaces.Data;
using ECommerce.Customers.Domain.Models;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Threading.Tasks;

namespace ECommerce.Customers.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext, IUnitOfWork
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<Email> Emails { get; set; }
        public DbSet<Phone> Phones { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Customer");

            modelBuilder.Ignore<ValidationResult>();

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }

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
    }
}
