using ECommerce.Baskets.Domain.Interfaces.Repositories;
using ECommerce.Baskets.Domain.Models;
using ECommerce.Baskets.Infrastructure.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Baskets.Infrastructure.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly ApplicationDbContext _context;

        public ItemRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Create(IEnumerable<Item> item)
        {
            await _context.Items.AddRangeAsync(item);
        }

        public Task Update(IEnumerable<Item> item)
        {
            _context.Items.UpdateRange(item);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
