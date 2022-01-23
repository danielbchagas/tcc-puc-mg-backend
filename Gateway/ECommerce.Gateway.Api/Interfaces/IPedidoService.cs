using ECommerce.Gateway.Api.Models.Pedido;
using Refit;
using System;
using System.Threading.Tasks;

namespace ECommerce.Gateway.Api.Interfaces
{
    public interface IPedidoService
    {
        [Post("/api/pedidos")]
        Task<IApiResponse> Create(PedidoDto pedido, [Authorize("Bearer")] string token);

        [Get("/api/pedidos/{id}")]
        Task<ApiResponse<PedidoDto>> Get(Guid id, [Authorize("Bearer")] string token);
    }
}
