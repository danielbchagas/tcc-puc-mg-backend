using System;
using System.Threading.Tasks;
using ECommerce.Basket.Domain.Interfaces.Data;
using ECommerce.Basket.Domain.Interfaces.Repositories;
using ECommerce.Basket.Domain.Models;
using ECommerce.Basket.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Basket.Infrastructure.Repositories
{
    public class BasketItemRepository : IBasketItemRepository
    {
        private readonly ApplicationDbContext _context;

        public BasketItemRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task Create(BasketItem item)
        {
            await _context.BasketItems.AddAsync(item);
        }

        public Task Update(BasketItem item)
        {
            _context.BasketItems.Update(item);

            return Task.CompletedTask;
        }

        public async Task<BasketItem> GetByProductId(Guid productId)
        {
            return await _context.BasketItems.FirstOrDefaultAsync(ic => ic.ProductId == productId);
        }

        public async Task<BasketItem> Get(Guid id)
        {
            return await _context.BasketItems.FindAsync(id);
        }

        public async Task Delete(Guid id)
        {
            var item = await Get(id);

            _context.BasketItems.Remove(item);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
