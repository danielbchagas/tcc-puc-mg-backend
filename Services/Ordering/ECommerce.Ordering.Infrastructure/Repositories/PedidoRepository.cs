using ECommerce.Core.Models.Ordering;
using ECommerce.Ordering.Domain.Interfaces.Data;
using ECommerce.Ordering.Domain.Interfaces.Repositories;
using ECommerce.Ordering.Infrastructure.Data;
using System;
using System.Threading.Tasks;

namespace ECommerce.Ordering.Infrastructure.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly ApplicationDbContext _context;

        public PedidoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<Order> Get(Guid id)
        {
            return await _context.Ordering.FindAsync(id);
        }

        public async Task Create(Order order)
        {
            await _context.Ordering.AddAsync(order);
        }

        public Task Update(Order order)
        {
            _context.Ordering.Update(order);
            
            return Task.CompletedTask;
        }
    }
}
