using ECommerce.Core.Contracts.Data;
using ECommerce.Core.Models.Customer;
using System;
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
    }
}
