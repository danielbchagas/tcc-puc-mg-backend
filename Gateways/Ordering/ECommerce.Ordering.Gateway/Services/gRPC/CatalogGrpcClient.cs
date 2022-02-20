using ECommerce.Catalog.Api.Protos;
using ECommerce.Ordering.Gateway.Interfaces;
using System.Threading.Tasks;

namespace ECommerce.Ordering.Gateway.Services.gRPC
{
    public class CatalogGrpcClient : ICatalogGrpcClient
    {
        private readonly CatalogService.CatalogServiceClient _client;

        public CatalogGrpcClient(CatalogService.CatalogServiceClient client)
        {
            _client = client;
        }

        public async Task<GetProductResponse> GetProduct(GetProductRequest request)
        {
            return await _client.GetProductAsync(request);
        }

        public async Task<UpdateProductResponse> UpdateProduct(UpdateProductRequest request)
        {
            return await _client.UpdateProductAsync(request);
        }
    }
}
