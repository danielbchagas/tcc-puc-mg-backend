using ECommerce.Customers.Domain.Interfaces.Data;
using ECommerce.Customers.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Customers.Domain.Interfaces.Repositories
{
    public interface IUserRepository : IDisposable
    {
        IUnitOfWork UnitOfWork { get; }
        Task Create(Customer person);
        Task Update(Customer person);
        Task Delete(Guid id);
        Task<Customer> Get(Guid id);
        Task<IList<Customer>> Get();
    }
}
