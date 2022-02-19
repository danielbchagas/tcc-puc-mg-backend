using System;
using System.Threading.Tasks;
using ECommerce.Basket.Domain.Interfaces.Data;
using ECommerce.Basket.Domain.Models;

namespace ECommerce.Basket.Domain.Interfaces.Repositories
{
    public interface IShoppingBasketRepository : IDisposable
    {
        IUnitOfWork UnitOfWork { get; }

        Task<ShoppingBasket> Get(Guid id);
        Task Create(ShoppingBasket basket);
        Task Update(ShoppingBasket basket);
        Task Delete(Guid id);
        Task<ShoppingBasket> GetByCustomer(Guid customerId);
    }
}
