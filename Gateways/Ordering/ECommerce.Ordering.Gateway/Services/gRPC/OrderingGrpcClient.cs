using ECommerce.Ordering.Api.Protos;
using ECommerce.Ordering.Gateway.Interfaces;
using System.Threading.Tasks;

namespace ECommerce.Ordering.Gateway.Services.gRPC
{
    public class OrderingGrpcClient : IOrderingGrpcClient
    {
        private readonly Ordering.Api.Protos.OrderingService.OrderingServiceClient _client;

        public OrderingGrpcClient(Ordering.Api.Protos.OrderingService.OrderingServiceClient client)
        {
            _client = client;
        }

        public async Task<CreateOrderResponse> Create(CreateOrderRequest request)
        {
            return await _client.CreateOrderAsync(request);
        }
    }
}
