using ECommerce.Pedido.Domain.Interfaces.Data;
using System;
using System.Threading.Tasks;
using PedidoCliente = ECommerce.Pedido.Domain.Models.Pedido;

namespace ECommerce.Pedido.Domain.Interfaces.Repositories
{
    public interface IPedidoRepository
    {
        IUnitOfWork UnitOfWork { get; }

        Task<PedidoCliente> Buscar(Guid id);
        Task Adicionar(PedidoCliente pedido);
        Task Atualizar(PedidoCliente pedido);
    }
}
