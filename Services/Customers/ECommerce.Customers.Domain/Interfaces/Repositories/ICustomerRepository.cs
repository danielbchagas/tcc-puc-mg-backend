using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ECommerce.Customers.Domain.Interfaces.Data;
using ECommerce.Customers.Domain.Models;

namespace ECommerce.Customers.Domain.Interfaces.Repositories
{
    public interface ICustomerRepository : IDisposable
    {
        IUnitOfWork UnitOfWork { get; }
        Task Create(Customer customer);
        Task Update(Customer customer);
        Task Delete(Guid id);
        Task<Customer> Get(Guid id);
        Task<IEnumerable<Customer>> Get();
        Task<IEnumerable<Customer>> Get(int page, int rows);
        Task<IEnumerable<Customer>> Get(Expression<Func<Customer, bool>> filter);
        Task<IEnumerable<Customer>> Get(Expression<Func<Customer, bool>> filter, int page, int rows);
    }
}
