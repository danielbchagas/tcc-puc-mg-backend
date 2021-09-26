using ECommerce.Compras.Gateway.Models;
using ECommerce.Compras.Gateway.Models.Pedido;
using System;
using System.Threading.Tasks;

namespace ECommerce.Compras.Gateway.Interfaces
{
    public interface IPedidoService
    {
        Task<ServiceResponse> Adicionar(PedidoDto pedido);
        Task<PedidoDto> Buscar(Guid id);
    }
}
