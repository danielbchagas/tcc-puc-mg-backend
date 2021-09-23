using ECommerce.Pedido.Domain.Interfaces.Data;
using ECommerce.Pedido.Domain.Interfaces.Repositories;
using ECommerce.Pedido.Infrastructure.Data;
using System;
using System.Threading.Tasks;
using PedidoCliente = ECommerce.Pedido.Domain.Models.Pedido;

namespace ECommerce.Pedido.Infrastructure.Repositories
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
            return await _context.Pedidos.FindAsync(id);
        }

        public async Task Adicionar(PedidoCliente pedido)
        {
            await _context.Pedidos.AddAsync(pedido);
        }

        public Task Atualizar(PedidoCliente pedido)
        {
            _context.Pedidos.Update(pedido);
            
            return Task.CompletedTask;
        }

        public async Task Excluir(Guid id)
        {
            var pedido = await _context.Pedidos.FindAsync(id);
            
            _context.Pedidos.Remove(pedido);
        }
    }
}
