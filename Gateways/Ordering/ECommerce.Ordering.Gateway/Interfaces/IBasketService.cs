using ECommerce.Basket.Domain.Models;
using Refit;
using System;
using System.Threading.Tasks;

namespace ECommerce.Ordering.Gateway.Interfaces
{
    public interface IBasketService
    {
        #region Basket
        [Get("/api/customerBasket/{customerId}")]
        Task<ApiResponse<CustomerBasket>> GetCustomerBasket(Guid customerId, [Authorize("Bearer")] string accessToken);

        [Post("/api/customerBasket")]
        Task<ApiResponse<object>> CreateCustomerBasket(CustomerBasket customerBasket, [Authorize("Bearer")] string accessToken);

        [Delete("/api/customerBasket/{customerBasketId}")]
        Task<ApiResponse<object>> DeleteCustomerBasket(Guid customerBasketId, [Authorize("Bearer")] string accessToken);
        #endregion

        #region Basket Item
        [Post("/api/basketItem")]
        Task<ApiResponse<object>> CreateBasketItem(BasketItem basketItem, [Authorize("Bearer")] string accessToken);

        [Delete("/api/basketItem/{id}")]
        Task<ApiResponse<object>> DeleteBasketItem(Guid id, [Authorize("Bearer")] string accessToken);

        [Get("/api/basketItem/{id}")]
        Task<ApiResponse<BasketItem>> GetBasketItem(Guid id, [Authorize("Bearer")] string accessToken);
        #endregion
    }
}
