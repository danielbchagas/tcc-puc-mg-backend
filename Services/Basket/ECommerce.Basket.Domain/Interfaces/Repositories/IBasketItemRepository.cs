using ECommerce.Basket.Domain.Interfaces.Data;
using ECommerce.Core.Models.Basket;
using System;
using System.Threading.Tasks;

namespace ECommerce.Basket.Domain.Interfaces.Repositories
{
    public interface IBasketItemRepository : IDisposable
    {
        IUnitOfWork UnitOfWork { get; }
        Task<BasketItem> Get(Guid id);
        Task Delete(Guid id);
        Task Create(BasketItem item);
        Task Update(BasketItem item);
        Task<BasketItem> GetByProductId(Guid productId);
    }
}
