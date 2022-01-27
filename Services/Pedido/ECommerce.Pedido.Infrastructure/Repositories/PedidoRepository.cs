using System;
using System.Threading.Tasks;
using ECommerce.Ordering.Domain.Interfaces.Data;
using ECommerce.Ordering.Domain.Interfaces.Repositories;
using ECommerce.Ordering.Infrastructure.Data;
using PedidoCliente = ECommerce.Ordering.Domain.Models.Order;

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

        public async Task<PedidoCliente> Buscar(Guid id)
        {
            return await _context.Ordering.FindAsync(id);
        }

        public async Task Adicionar(PedidoCliente pedido)
        {
            await _context.Ordering.AddAsync(pedido);
        }

        public Task Atualizar(PedidoCliente pedido)
        {
            _context.Ordering.Update(pedido);
            
            return Task.CompletedTask;
        }
    }
}
