using ECommerce.Core.Contracts.Data;
using ECommerce.Core.Models.Catalog;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ECommerce.Catalog.Domain.Interfaces.Repositories
{
    public interface IProductRepository : IDisposable
    {
        IUnitOfWork UnitOfWork { get; }
        Task Create(Product product);
        Task Update(Product product);
        Task Delete(Guid id);
        Task<Product> Get(Guid id);
        Task<IEnumerable<Product>> Get();
        Task<IEnumerable<Product>> Get(Expression<Func<Product, bool>> filter);
    }
}
