﻿using ECommerce.Gateway.Api.Models;
using Refit;
using System;
using System.Threading.Tasks;

namespace ECommerce.Gateway.Api.Interfaces
{
    public interface IBasketService
    {
        [Get("/api/customerbasket/{customerId}")]
        Task<ApiResponse<CustomerBasketDto>> GetCustomerBasket(Guid customerId, [Authorize("Bearer")] string accessToken);
        
        [Post("/api/customerbasket")]
        Task<ApiResponse<object>> CreateCustomerBasket(CustomerBasketDto customerBasket, [Authorize("Bearer")] string accessToken);
        
        [Delete("/api/customerbasket/{customerBasketId}")]
        Task<ApiResponse<object>> DeleteCustomerBasket(Guid customerBasketId, [Authorize("Bearer")] string accessToken);
        
        [Post("/api/basketItemId")]
        Task<ApiResponse<object>> CreateBasketItem(BasketItemDto basketItem, [Authorize("Bearer")] string accessToken);
        
        [Delete("/api/basketitem/{basketItemId}")]
        Task<ApiResponse<object>> DeleteBasketItem(Guid basketItemId, [Authorize("Bearer")] string accessToken);
    }
}
