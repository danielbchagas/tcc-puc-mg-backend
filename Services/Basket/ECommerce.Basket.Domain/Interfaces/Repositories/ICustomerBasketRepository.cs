using ECommerce.Basket.Domain.Interfaces.Data;
using ECommerce.Core.Models.Basket;
using System;
using System.Threading.Tasks;

namespace ECommerce.Basket.Domain.Interfaces.Repositories
{
    public interface ICustomerBasketRepository : IDisposable
    {
        IUnitOfWork UnitOfWork { get; }

        Task<CustomerBasket> Get(Guid id);
        Task Create(CustomerBasket basket);
        Task Update(CustomerBasket basket);
        Task Delete(Guid id);
        
        Task<CustomerBasket> GetByCustomerId(Guid customerId);
    }
}
