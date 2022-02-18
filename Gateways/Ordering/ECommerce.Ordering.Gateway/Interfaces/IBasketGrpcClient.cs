using ECommerce.Basket.Domain.Models;
using System;
using System.Threading.Tasks;

namespace ECommerce.Ordering.Gateway.Interfaces
{
    public interface IBasketGrpcClient
    {
        Task<CustomerBasket> GetCustomerBasket(Guid customerId);
    }
}