using ECommerce.Customers.Domain.Models;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ECommerce.Customers.Domain.Interfaces.Repositories
{
    public interface ICustomerRepository : IDisposable
    {
        Task Create(Customer person);
        Task Update(Customer person);
        Task Delete(Guid id);
        Task<Customer> Get(Guid id);
        Task<IList<Customer>> Get();
        Task<IList<Customer>> GetData(
            Expression<Func<Customer, bool>> expression = null, 
            Func<IQueryable<Customer>, IIncludableQueryable<Customer, object>> includes = null, 
            int? skip = null, 
            int? take = null);
    }
}
