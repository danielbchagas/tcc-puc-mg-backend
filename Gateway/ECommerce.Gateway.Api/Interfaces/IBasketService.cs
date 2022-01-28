using ECommerce.Gateway.Api.Models;
using Refit;
using System;
using System.Threading.Tasks;

namespace ECommerce.Gateway.Api.Interfaces
{
    public interface IBasketService
    {
        [Get("/api/baskets/{id}")]
        Task<ApiResponse<CustomerBasketDto>> GetCarrinho(Guid id, [Authorize("Bearer")] string accessToken);
        
        [Post("/api/baskets")]
        Task<ApiResponse<object>> Create(CustomerBasketDto dto, [Authorize("Bearer")] string accessToken);
        
        [Delete("/api/baskets/{id}")]
        Task<ApiResponse<object>> DeleteCarrinho(Guid id, [Authorize("Bearer")] string accessToken);
        
        [Post("/api/basketItems")]
        Task<ApiResponse<object>> CreateItemCarrinho(BasketItemDto dto, [Authorize("Bearer")] string accessToken);
        
        [Delete("/api/basketItems/{id}")]
        Task<ApiResponse<object>> DeleteItemCarrinho(Guid id, [Authorize("Bearer")] string accessToken);
    }
}
