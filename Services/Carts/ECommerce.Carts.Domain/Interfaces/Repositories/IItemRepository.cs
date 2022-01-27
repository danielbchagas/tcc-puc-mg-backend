using System;
using System.Threading.Tasks;
using ECommerce.Carts.Domain.Interfaces.Data;
using ECommerce.Carts.Domain.Models;

namespace ECommerce.Carts.Domain.Interfaces.Repositories
{
    public interface IItemRepository : IDisposable
    {
        IUnitOfWork UnitOfWork { get; }
        
        Task Delete(Guid id);
        Task Create(Item item);
        Task Update(Item item);

        Task<Item> GetByProductId(Guid productId);
    }
}
