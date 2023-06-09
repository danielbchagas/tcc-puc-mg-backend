using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ECommerce.Baskets.Domain.Interfaces.Repositories
{
    public interface IBasketRepository : IDisposable
    {
        Task<Models.Basket> Get(Guid id);
        Task Create(Models.Basket basket);
        Task Update(Models.Basket basket);
        Task Delete(Guid id);
        Task<Models.Basket> GetByCustomer(Guid customerId);
        Task<IList<Models.Basket>> Filter(Expression<Func<Models.Basket, bool>> expression);
    }
}
