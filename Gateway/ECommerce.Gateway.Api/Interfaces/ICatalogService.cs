using ECommerce.Gateway.Api.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Gateway.Api.Interfaces
{
    public interface ICatalogService
    {
        [Get("/api/products/{id}")]
        Task<ApiResponse<ProductDto>> Get(Guid id);

        [Get("/api/products")]
        Task<ApiResponse<IEnumerable<ProductDto>>> Get();

        [Get("/api/products")]
        Task<ApiResponse<IEnumerable<ProductDto>>> Get(string product);

        [Put("/api/products")]
        Task<IApiResponse> Update(ProductDto product, [Authorize("Bearer")] string accessToken);
    }
}
