using ECommerce.Basket.Api.Protos;
using ECommerce.Ordering.Gateway.Interfaces;
using System;
using System.Threading.Tasks;

namespace ECommerce.Ordering.Gateway.Services.gRPC
{
    public class BasketGrpcClient : IBasketGrpcClient
    {
        private readonly ShoppingBasketService.ShoppingBasketServiceClient _client;

        public BasketGrpcClient(ShoppingBasketService.ShoppingBasketServiceClient client)
        {
            _client = client;
        }

        public async Task<CreateBasketResponse> CreateShoppingBasket(CreateBasketRequest request)
        {
            return await _client.CreateBasketAsync(request);
        }

        public async Task<GetBasketResponse> GetShoppingBasketByCustomer(Guid customerId)
        {
            return await _client.GetBasketByCustomerAsync(new GetBasketByCustomerRequest { Customerid = Convert.ToString(customerId) });
        }

        public async Task<DeleteBasketResponse> DeleteShoppingBasket(Guid id)
        {
            var basketConsumer = new DeleteBasketRequest
            {
                Id = Convert.ToString(id)
            };

            return await _client.DeleteBasketAsync(basketConsumer);
        }
    }
}
