using ECommerce.Catalog.Domain.Models;
using Refit;
using System;
using System.Threading.Tasks;

namespace ECommerce.Ordering.Gateway.Interfaces
{
    public interface ICatalogService
    {
        [Get("/api/product/{id}")]
        Task<ApiResponse<Product>> Get(Guid id);

        [Put("/api/product/{id}")]
        Task<IApiResponse> Update(Guid id, Product product, [Authorize("Bearer")] string accessToken);
    }
}
