using ECommerce.Basket.Domain.Interfaces.Data;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ECommerce.Basket.Domain.Interfaces.Repositories
{
    public interface IBasketRepository : IDisposable
    {
        IUnitOfWork UnitOfWork { get; }

        Task<Models.Basket> Get(Guid id);
        Task Create(Models.Basket basket);
        Task Update(Models.Basket basket);
        Task Delete(Guid id);
        Task<Models.Basket> GetByCustomer(Guid customerId);
        Task<IList<Models.Basket>> Filter(Expression<Func<Models.Basket, bool>> expression);
    }
}
