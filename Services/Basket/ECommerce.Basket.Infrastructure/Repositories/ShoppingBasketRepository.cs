using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ECommerce.Basket.Domain.Interfaces.Data;
using ECommerce.Basket.Domain.Interfaces.Repositories;
using ECommerce.Basket.Domain.Models;
using ECommerce.Basket.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Basket.Infrastructure.Repositories
{
    public class ShoppingBasketRepository : IShoppingBasketRepository
    {
        private readonly ApplicationDbContext _context;

        public ShoppingBasketRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task Create(ShoppingBasket basket)
        {
            await _context.ShoppingBaskets.AddAsync(basket);
        }

        public Task Update(ShoppingBasket basket)
        {
            _context.ShoppingBaskets.Update(basket);
            return Task.CompletedTask;
        }

        public async Task<ShoppingBasket> Get(Guid id)
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

        public async Task<ShoppingBasket> GetByCustomer(Guid customerId)
        {
            return await _context.ShoppingBaskets
                .Include(cb => cb.Items)
                .FirstOrDefaultAsync(cb => cb.CustomerId == customerId);
        }

        public async Task<IList<ShoppingBasket>> Filter(Expression<Func<ShoppingBasket, bool>> expression)
        {
            return await _context.ShoppingBaskets
                .Where(expression)
                .ToListAsync();
        }
    }
}
