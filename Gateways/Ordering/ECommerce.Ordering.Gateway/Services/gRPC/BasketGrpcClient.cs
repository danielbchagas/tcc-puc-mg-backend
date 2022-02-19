using ECommerce.Catalog.Api.Protos;
using ECommerce.Ordering.Gateway.Interfaces;
using ECommerce.Ordering.Gateway.Models;
using System;
using System.Threading.Tasks;

namespace ECommerce.Ordering.Gateway.Services.gRPC
{
    public class BasketGrpcClient : IBasketGrpcClient
    {
        private readonly CustomerBasket.CustomerBasketClient _client;

        public BasketGrpcClient(CustomerBasket.CustomerBasketClient client)
        {
            _client = client;
        }

        public async Task<CreateBasketResponse> CreateCustomerBasket(CustomerBasketDTO customerBasket)
        {
            var basketConsumer = new CreateBasketRequest
            {
                Customerid = Convert.ToString(customerBasket.CustomerId)
            };

            return await _client.CreateBasketAsync(basketConsumer);
        }

        public async Task<GetBasketResponse> GetCustomerBasket(Guid customerId)
        {
            return await _client.GetBasketAsync(new GetBasketRequest { Customerid = Convert.ToString(customerId) });
        }

        public async Task<DeleteBasketResponse> DeleteCustomerBasket(Guid id)
        {
            var basketConsumer = new DeleteBasketRequest
            {
                Id = Convert.ToString(id)
            };

            return await _client.DeleteBasketAsync(basketConsumer);
        }
    }
}
