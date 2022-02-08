using System;
using System.Threading.Tasks;
using ECommerce.Ordering.Domain.Interfaces.Data;
using ECommerce.Ordering.Domain.Interfaces.Repositories;
using ECommerce.Ordering.Domain.Models;
using ECommerce.Ordering.Infrastructure.Data;

namespace ECommerce.Ordering.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<Order> Buscar(Guid id)
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
