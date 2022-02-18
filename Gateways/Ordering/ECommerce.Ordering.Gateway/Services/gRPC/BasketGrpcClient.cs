using ECommerce.Catalog.Api.Protos;
using ECommerce.Ordering.Gateway.Interfaces;
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

        public async Task<Basket.Domain.Models.CustomerBasket> GetCustomerBasket(Guid customerId)
        {
            var response = await _client.GetBasketAsync(new GetBasketRequest { Customerid = Convert.ToString(customerId)});

            var basket = new Basket.Domain.Models.CustomerBasket(new Guid(response.Customerid))
            {
                Id = new Guid(response.Id),
                Value = Convert.ToDecimal(response.Value)
            };

            foreach(var item in response.Items)
            {
                basket.Items.Add(new Basket.Domain.Models.BasketItem(
                    id: new Guid(item.Id),
                    name: item.Name,
                    quantity: item.Quantity,
                    value: Convert.ToDecimal(item.Value),
                    image: item.Image,
                    customerBasketId: new Guid(item.Basketid)
                ));
            }

            return basket;
        }
    }
}
