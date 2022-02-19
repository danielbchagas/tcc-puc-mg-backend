using System;
using System.Threading.Tasks;
using ECommerce.Basket.Domain.Interfaces.Data;
using ECommerce.Basket.Domain.Interfaces.Repositories;
using ECommerce.Basket.Domain.Models;
using ECommerce.Basket.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Basket.Infrastructure.Repositories
{
    public class CustomerBasketRepository : ICustomerBasketRepository
    {
        private readonly ApplicationDbContext _context;

        public CustomerBasketRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task Create(CustomerBasket basket)
        {
            await _context.CustomerBaskets.AddAsync(basket);
        }

        public Task Update(CustomerBasket basket)
        {
            _context.CustomerBaskets.Update(basket);
            return Task.CompletedTask;
        }

        public async Task<CustomerBasket> Get(Guid customerId)
        {
            return await _context.CustomerBaskets
                .Include(cb => cb.Items)
                .FirstOrDefaultAsync(cb => cb.CustomerId == customerId);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

        public async Task Delete(Guid customerId)
        {
            var cart = await Get(customerId);

            _context.CustomerBaskets.Remove(cart);
        }
    }
}
