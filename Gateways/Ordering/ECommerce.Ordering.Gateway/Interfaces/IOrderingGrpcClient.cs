using System.Threading.Tasks;

namespace ECommerce.Ordering.Gateway.Interfaces
{
    public interface IOrderingGrpcClient
    {
        Task<Ordering.Api.Protos.CreateOrderResponse> CreateOrder(Ordering.Api.Protos.CreateOrderRequest request);
    }
}
