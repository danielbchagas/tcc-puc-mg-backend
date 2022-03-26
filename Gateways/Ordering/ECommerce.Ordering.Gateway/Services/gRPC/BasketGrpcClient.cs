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

        #region ShoppingBasket
        public async Task<CreateBasketResponse> CreateShoppingBasket(CreateBasketRequest request)
        {
            return await _client.CreateBasketAsync(request);
        }

        public async Task<GetBasketByCustomerResponse> GetShoppingBasketByCustomer(GetBasketByCustomerRequest request)
        {
            return await _client.GetBasketByCustomerAsync(request);
        }

        public async Task<DeleteBasketResponse> DeleteShoppingBasket(DeleteBasketRequest request)
        {
            return await _client.DeleteBasketAsync(request);
        }
        #endregion

        #region BasketItem
        public async Task<GetBasketItemResponse> GetBasketItem(GetBasketItemRequest request)
        {
            return await _client.GetBasketItemAsync(request);
        }

        public async Task<GetBasketItemByProductResponse> GetBasketItemByProduct(GetBasketItemByProductRequest request)
        {
            return await _client.GetBasketItemByProductAsync(request);
        }

        public async Task<AddBasketItemResponse> AddBasketItem(AddBasketItemRequest request)
        {
            return await _client.AddBasketItemAsync(request);
        }

        public async Task<RemoveBasketItemResponse> RemoveBasketItem(RemoveBasketItemRequest request)
        {
            return await _client.RemoveBasketItemAsync(request);
        }
        #endregion
    }
}
