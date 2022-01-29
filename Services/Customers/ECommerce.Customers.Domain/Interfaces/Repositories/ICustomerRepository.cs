using ECommerce.Customers.Domain.Interfaces.Data;
using ECommerce.Customers.Domain.Models;
using System;
using System.Threading.Tasks;

namespace ECommerce.Customers.Domain.Interfaces.Repositories
{
    public interface ICustomerRepository : IDisposable
    {
        IUnitOfWork UnitOfWork { get; }
        Task Create(Customer customer);
        Task Update(Customer customer);
        Task Delete(Guid id);
        Task<Customer> Get(Guid id);
    }
}
