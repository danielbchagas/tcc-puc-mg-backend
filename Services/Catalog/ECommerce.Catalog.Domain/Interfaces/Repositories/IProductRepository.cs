using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ECommerce.Catalog.Domain.Interfaces.Data;
using ECommerce.Catalog.Domain.Models;

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
        Task<IEnumerable<Product>> Get(int page, int rows);
        Task<IEnumerable<Product>> Get(Expression<Func<Product, bool>> filter, int page, int rows);
    }
}
