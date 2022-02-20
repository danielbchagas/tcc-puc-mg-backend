using System.Threading.Tasks;

namespace ECommerce.Ordering.Gateway.Interfaces
{
    public interface IOrderingGrpcClient
    {
        Task<Ordering.Api.Protos.CreateOrderResponse> Create(Ordering.Api.Protos.CreateOrderRequest request);
    }
}
