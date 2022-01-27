using System;
using System.Threading.Tasks;
using ECommerce.Carts.Domain.Interfaces.Data;
using ECommerce.Carts.Domain.Interfaces.Repositories;
using ECommerce.Carts.Domain.Models;
using ECommerce.Carts.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Carts.Infrastructure.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly ApplicationDbContext _context;

        public ItemRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task Create(Item item)
        {
            await _context.Items.AddAsync(item);
        }

        public Task Update(Item item)
        {
            _context.Items.Update(item);

            return Task.CompletedTask;
        }

        public async Task<Item> GetByProductId(Guid productId)
        {
            return await _context.Items.FirstOrDefaultAsync(ic => ic.ProductId == productId);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

        public async Task Delete(Guid id)
        {
            var item = await _context.Items.FindAsync(id);

            _context.Items.Remove(item);
        }
    }
}
