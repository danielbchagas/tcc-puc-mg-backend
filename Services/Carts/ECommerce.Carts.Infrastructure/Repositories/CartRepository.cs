using System;
using System.Threading.Tasks;
using ECommerce.Carts.Domain.Interfaces.Data;
using ECommerce.Carts.Domain.Interfaces.Repositories;
using ECommerce.Carts.Domain.Models;
using ECommerce.Carts.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Carts.Infrastructure.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext _context;

        public CartRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task Create(Cart cart)
        {
            await _context.Carts.AddAsync(cart);
        }

        public Task Update(Cart cart)
        {
            _context.Carts.Update(cart);
            return Task.CompletedTask;
        }

        public async Task<Cart> Get(Guid id)
        {
            return await _context.Carts.FindAsync(id);
        }

        public async Task<Cart> GetByCustomerId(Guid customerId)
        {
            return await _context.Carts
                .Include(cc => cc.Itens)
                .FirstOrDefaultAsync(cc => cc.CustomerId == customerId);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

        public async Task Delete(Guid id)
        {
            var cart = await _context.Carts.FindAsync(id);

            _context.Carts.Remove(cart);
        }
    }
}
