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

        [Get("/api/products/{page}/{rows}")]
        Task<ApiResponse<IEnumerable<ProductDto>>> Get(int page, int rows);

        [Get("/api/products/{name}/{page}/{rows}")]
        Task<ApiResponse<IEnumerable<ProductDto>>> Get(string name, int page, int rows);

        [Put("/api/products")]
        Task<IApiResponse> Update(ProductDto product, [Authorize("Bearer")] string accessToken);
    }
}
