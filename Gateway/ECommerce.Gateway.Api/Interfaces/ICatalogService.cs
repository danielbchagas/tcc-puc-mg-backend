using ECommerce.Gateway.Api.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Gateway.Api.Interfaces
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
