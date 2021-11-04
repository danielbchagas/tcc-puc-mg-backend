using ECommerce.Compras.Gateway.Models.Pedido;
using Refit;
using System;
using System.Threading.Tasks;

namespace ECommerce.Compras.Gateway.Interfaces
{
    public interface IPedidoService
    {
        [Post("/api/pedidos")]
        Task<ApiResponse<string>> Adicionar(PedidoDto pedido);

        [Get("/api/pedidos/{id}")]
        Task<ApiResponse<PedidoDto>> Buscar(Guid id);
    }
}
