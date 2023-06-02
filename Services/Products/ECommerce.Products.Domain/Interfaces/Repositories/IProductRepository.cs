using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ECommerce.Products.Domain.Interfaces.Data;
using ECommerce.Products.Domain.Models;

namespace ECommerce.Products.Domain.Interfaces.Repositories
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
