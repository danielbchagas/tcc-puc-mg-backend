using ECommerce.Catalog.Domain.Interfaces.Repositories;
using ECommerce.Catalog.Infrastructure.Data;
using ECommerce.Core.Contracts.Data;
using ECommerce.Core.Models.Catalog;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ECommerce.Catalog.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        private readonly ApplicationDbContext _context;

        public IUnitOfWork UnitOfWork => _context;

        public async Task Create(Product product)
        {
            await _context.Products.AddAsync(product);
        }

        public Task Update(Product product)
        {
            _context.Products.Update(product);

            return Task.CompletedTask;
        }

        public async Task<Product> Get(Guid id)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Product>> Get()
        {
            return await _context.Products.ToListAsync();
        }
        
        public async Task<IEnumerable<Product>> Get(Expression<Func<Product, bool>> filter)
        {
            return await _context.Products
                .Where(filter)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task Delete(Guid id)
        {
            var product = await Get(id);
            _context.Products.Remove(product);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
