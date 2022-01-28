using ECommerce.Gateway.Api.Models;
using Refit;
using System;
using System.Threading.Tasks;

namespace ECommerce.Gateway.Api.Interfaces
{
    public interface IOrderingService
    {
        [Post("/api/ordering")]
        Task<IApiResponse> Create(OrderDto order, [Authorize("Bearer")] string accessToken);

        [Get("/api/ordering/{id}")]
        Task<ApiResponse<OrderDto>> GetOrder(Guid id, [Authorize("Bearer")] string accessToken);
    }
}
