using ECommerce.Ordering.Gateway.Models;
using Refit;
using System;
using System.Threading.Tasks;

namespace ECommerce.Ordering.Gateway.Interfaces
{
    public interface ICatalogService
    {
        [Get("/api/product/{id}")]
        Task<ApiResponse<ProductDto>> Get(Guid id);

        [Put("/api/product/{id}")]
        Task<IApiResponse> Update(Guid id, ProductDto product, [Authorize("Bearer")] string accessToken);
    }
}
