using ECommerce.Compras.Gateway.Models.Carrinho;
using Refit;
using System;
using System.Threading.Tasks;

namespace ECommerce.Compras.Gateway.Interfaces
{
    public interface ICarrinhoService
    {
        // Carrinho
        [Get("/api/carrinhos/{id}")]
        Task<ApiResponse<CarrinhoDto>> GetCarrinho(Guid id, [Authorize("Bearer")] string token);
        
        [Post("/api/carrinhos")]
        Task<IApiResponse> Create(CarrinhoDto dto, [Authorize("Bearer")] string token);
        
        [Delete("/api/carrinhos/{id}")]
        Task<IApiResponse> DeleteCarrinho(Guid id, [Authorize("Bearer")] string token);

        // Item carrinho
        [Post("/api/itenscarrinhos")]
        Task<IApiResponse> CreateItemCarrinho(ItemCarrinhoDto dto, [Authorize("Bearer")] string token);
        
        [Delete("/api/itenscarrinhos/{id}")]
        Task<IApiResponse> DeleteItemCarrinho(Guid id, [Authorize("Bearer")] string token);
    }
}
