using System;
using System.Threading.Tasks;
using ECommerce.Ordering.Domain.Interfaces.Data;
using PedidoCliente = ECommerce.Ordering.Domain.Models.Order;

namespace ECommerce.Ordering.Domain.Interfaces.Repositories
{
    public interface IPedidoRepository
    {
        IUnitOfWork UnitOfWork { get; }

        Task<PedidoCliente> Buscar(Guid id);
        Task Adicionar(PedidoCliente pedido);
        Task Atualizar(PedidoCliente pedido);
    }
}
