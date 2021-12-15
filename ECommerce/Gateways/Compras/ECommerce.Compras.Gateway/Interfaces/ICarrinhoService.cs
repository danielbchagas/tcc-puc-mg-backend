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
        Task<ApiResponse<CarrinhoDto>> BuscarCarrinho(Guid id, [Authorize("Bearer")] string token);
        
        [Post("/api/carrinhos")]
        Task<ApiResponse<string>> AdicionarCarrinho(CarrinhoDto dto, [Authorize("Bearer")] string token);
        
        [Delete("/api/carrinhos/{id}")]
        Task<ApiResponse<string>> ExcluirCarrinho(Guid id, [Authorize("Bearer")] string token);

        // Item carrinho
        [Post("/api/itenscarrinhos")]
        Task<ApiResponse<string>> AdicionarItemCarrinho(ItemCarrinhoDto dto, [Authorize("Bearer")] string token);
        
        [Delete("/api/itenscarrinhos/{id}")]
        Task<ApiResponse<string>> ExcluirItemCarrinho(Guid id, [Authorize("Bearer")] string token);
    }
}
