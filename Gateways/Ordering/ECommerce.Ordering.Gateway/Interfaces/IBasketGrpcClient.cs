using ECommerce.Catalog.Api.Protos;
using ECommerce.Ordering.Gateway.Models;
using System;
using System.Threading.Tasks;

namespace ECommerce.Ordering.Gateway.Interfaces
{
    public interface IBasketGrpcClient
    {
        Task<CreateBasketResponse> CreateCustomerBasket(CustomerBasketDTO customerBasket);
        Task<GetBasketResponse> GetCustomerBasket(Guid customerId);
        Task<DeleteBasketResponse> DeleteCustomerBasket(Guid id);
    }
}