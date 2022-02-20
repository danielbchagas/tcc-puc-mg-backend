using System.Threading.Tasks;

namespace ECommerce.Ordering.Gateway.Interfaces
{
    public interface ICatalogGrpcClient
    {
        Task<Catalog.Api.Protos.GetProductResponse> GetProduct(Catalog.Api.Protos.GetProductRequest request);
        Task<Catalog.Api.Protos.UpdateProductResponse> UpdateProduct(Catalog.Api.Protos.UpdateProductRequest request);
    }
}
