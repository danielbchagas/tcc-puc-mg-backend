using ECommerce.Basket.Api.Protos;
using System;
using System.Threading.Tasks;

namespace ECommerce.Ordering.Gateway.Interfaces
{
    public interface IBasketGrpcClient
    {
        Task<CreateBasketResponse> CreateShoppingBasket(CreateBasketRequest request);
        Task<GetBasketResponse> GetShoppingBasketByCustomer(Guid customerId);
        Task<DeleteBasketResponse> DeleteShoppingBasket(Guid id);
    }
}