using System;
using System.Threading.Tasks;
using ECommerce.Customer.Domain.Interfaces.Data;
using ECommerce.Customer.Domain.Models;

namespace ECommerce.Customer.Domain.Interfaces.Repositories
{
    public interface IUserRepository : IDisposable
    {
        IUnitOfWork UnitOfWork { get; }
        Task Create(User person);
        Task Update(User person);
        Task Delete(Guid id);
        Task<User> Get(Guid id);
    }
}
