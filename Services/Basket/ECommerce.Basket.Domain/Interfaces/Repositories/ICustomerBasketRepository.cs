using System;
using System.Threading.Tasks;
using ECommerce.Basket.Domain.Interfaces.Data;
using ECommerce.Basket.Domain.Models;

namespace ECommerce.Basket.Domain.Interfaces.Repositories
{
    public interface ICustomerBasketRepository : IDisposable
    {
        IUnitOfWork UnitOfWork { get; }

        /// <summary>
        /// Get basket using custumerId
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns>Basket</returns>
        Task<CustomerBasket> Get(Guid customerId);

        /// <summary>
        /// Create new basket
        /// </summary>
        /// <param name="basket"></param>
        /// <returns></returns>
        Task Create(CustomerBasket basket);
        
        /// <summary>
        /// Update an existing basket
        /// </summary>
        /// <param name="basket"></param>
        /// <returns></returns>
        Task Update(CustomerBasket basket);

        /// <summary>
        /// Delete basket using customerId
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        Task Delete(Guid customerId);
    }
}
