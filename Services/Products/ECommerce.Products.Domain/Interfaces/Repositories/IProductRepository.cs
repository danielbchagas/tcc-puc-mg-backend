using ECommerce.Products.Domain.Models;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ECommerce.Products.Domain.Interfaces.Repositories
{
    public interface IProductRepository : IDisposable
    {
        Task Create(Product product);
        Task Update(Product product);
        Task Delete(Guid id);
        Task<Product> Get(Guid id);
        Task<IEnumerable<Product>> Get();
        Task<IEnumerable<Product>> Get(Expression<Func<Product, bool>> expression = null,
            Func<IQueryable<Product>, IIncludableQueryable<Product, object>> includes = null,
            int? skip = null,
            int? take = null);
    }
}
