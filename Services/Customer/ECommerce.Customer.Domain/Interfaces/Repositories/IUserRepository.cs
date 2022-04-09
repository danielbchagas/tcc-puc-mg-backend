using ECommerce.Customer.Domain.Interfaces.Data;
using ECommerce.Customer.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Customer.Domain.Interfaces.Repositories
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
