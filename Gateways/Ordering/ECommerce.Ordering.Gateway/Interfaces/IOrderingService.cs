using System;
using System.Threading.Tasks;
using ECommerce.Ordering.Gateway.Models;
using Refit;

namespace ECommerce.Ordering.Gateway.Interfaces
{
    public interface IOrderingService
    {
        [Post("/api/ordering")]
        Task<IApiResponse> Create(OrderDto order, [Authorize("Bearer")] string accessToken);

        [Get("/api/ordering/{id}")]
        Task<ApiResponse<OrderDto>> GetOrder(Guid id, [Authorize("Bearer")] string accessToken);
    }
}
