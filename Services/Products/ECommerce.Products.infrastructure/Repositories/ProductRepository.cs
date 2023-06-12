using ECommerce.Products.Domain.Interfaces.Repositories;
using ECommerce.Products.Domain.Models;
using ECommerce.Products.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ECommerce.Products.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        private readonly ApplicationDbContext _context;

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

        public async Task<IEnumerable<Product>> Get(Expression<Func<Product, bool>> expression = null, 
            Func<IQueryable<Product>, IIncludableQueryable<Product, object>> includes = null, 
            int? skip = null, 
            int? take = null)
        {
            var query = _context.Products.AsQueryable();

            if (expression != null)
                query = query.Where(expression);

            if (includes != null)
                query = includes(query);

            if (skip.HasValue)
                query = query.Skip(skip.Value);

            if (take.HasValue)
                query = query.Take(take.Value);

            return await query.ToListAsync();
        }

        public async Task<(Guid, string)> GetImage(Guid productId)
        {
            var result = await _context.Products
                .Where(p => p.Id == productId)
                .Select(p => new { Id = p.Id, Image = p.Image })
                .FirstOrDefaultAsync();

            return (result.Id, result.Image);
        }
    }
}
