using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ECommerce.Ordering.Gateway.Models;
using Refit;

namespace ECommerce.Ordering.Gateway.Interfaces
{
    public interface ICatalogService
    {
        [Get("/api/product/{id}")]
        Task<ApiResponse<ProductDto>> Get(Guid id);

        [Get("/api/product")]
        Task<ApiResponse<IEnumerable<ProductDto>>> Get();

        [Get("/api/product/{product}")]
        Task<ApiResponse<IEnumerable<ProductDto>>> Get(string product);

        [Put("/api/product")]
        Task<IApiResponse> Update(ProductDto product, [Authorize("Bearer")] string accessToken);
    }
}
