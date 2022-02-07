using ECommerce.Core.Models.Ordering;
using Refit;
using System;
using System.Threading.Tasks;

namespace ECommerce.Ordering.Gateway.Interfaces
{
    public interface IOrderingService
    {
        [Post("/api/ordering")]
        Task<IApiResponse> Create(Order order, [Authorize("Bearer")] string accessToken);

        [Get("/api/ordering/{id}")]
        Task<ApiResponse<Order>> GetOrder(Guid id, [Authorize("Bearer")] string accessToken);
    }
}
