using ECommerce.Basket.Domain.Interfaces.Data;
using ECommerce.Basket.Domain.Interfaces.Repositories;
using ECommerce.Basket.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ECommerce.Basket.Infrastructure.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly ApplicationDbContext _context;

        public BasketRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task Create(Domain.Models.Basket basket)
        {
            await _context.ShoppingBaskets.AddAsync(basket);
        }

        public Task Update(Domain.Models.Basket basket)
        {
            _context.ShoppingBaskets.Update(basket);
            return Task.CompletedTask;
        }

        public async Task<Domain.Models.Basket> Get(Guid id)
        {
            return await _context.ShoppingBaskets
                .Include(cb => cb.Items)
                .FirstOrDefaultAsync(cb => cb.Id == id);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

        public async Task Delete(Guid id)
        {
            var basket = await Get(id);

            _context.ShoppingBaskets.Remove(basket);
        }

        public async Task<Domain.Models.Basket> GetByCustomer(Guid customerId)
        {
            return await _context.ShoppingBaskets
                .Include(cb => cb.Items)
                .FirstOrDefaultAsync(cb => cb.CustomerId == customerId);
        }

        public async Task<IList<Domain.Models.Basket>> Filter(Expression<Func<Domain.Models.Basket, bool>> expression)
        {
            return await _context.ShoppingBaskets
                .Where(expression)
                .ToListAsync();
        }
    }
}
