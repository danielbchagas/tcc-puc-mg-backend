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

        public async Task<CustomerBasket> Get(Guid id)
        {
            return await _context.CustomerBaskets.FindAsync(id);
        }

        public async Task<CustomerBasket> GetByCustomerId(Guid customerId)
        {
            return await _context.CustomerBaskets
                .Include(cc => cc.Itens)
                .FirstOrDefaultAsync(cc => cc.CustomerId == customerId);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

        public async Task Delete(Guid id)
        {
            var cart = await _context.CustomerBaskets.FindAsync(id);

            _context.CustomerBaskets.Remove(cart);
        }
    }
}
