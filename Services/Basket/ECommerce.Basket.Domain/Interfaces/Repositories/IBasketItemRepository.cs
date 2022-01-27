using System;
using System.Threading.Tasks;
using ECommerce.Basket.Domain.Interfaces.Data;
using ECommerce.Basket.Domain.Models;

namespace ECommerce.Basket.Domain.Interfaces.Repositories
{
    public interface IBasketItemRepository : IDisposable
    {
        IUnitOfWork UnitOfWork { get; }
        
        Task Delete(Guid id);
        Task Create(BasketItem item);
        Task Update(BasketItem item);

        Task<BasketItem> GetByProductId(Guid productId);
    }
}
