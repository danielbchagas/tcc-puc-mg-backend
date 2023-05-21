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
        Task Create(User person);
        Task Update(User person);
        Task Delete(Guid id);
        Task<User> Get(Guid id);
        Task<IList<User>> Get();
    }
}
