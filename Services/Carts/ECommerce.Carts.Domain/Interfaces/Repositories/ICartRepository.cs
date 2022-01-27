using System;
using System.Threading.Tasks;
using ECommerce.Carts.Domain.Interfaces.Data;
using ECommerce.Carts.Domain.Models;

namespace ECommerce.Carts.Domain.Interfaces.Repositories
{
    public interface ICartRepository : IDisposable
    {
        IUnitOfWork UnitOfWork { get; }

        Task<Cart> Get(Guid id);
        Task Create(Cart cart);
        Task Update(Cart cart);
        Task Delete(Guid id);
        
        Task<Cart> GetByCustomerId(Guid customerId);
    }
}
