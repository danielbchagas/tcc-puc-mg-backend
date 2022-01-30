using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ECommerce.Customer.Domain.Interfaces.Data;
using ECommerce.Customer.Domain.Models;

namespace ECommerce.Customer.Domain.Interfaces.Repositories
{
    public interface IAddressRepository : IDisposable
    {
        IUnitOfWork UnitOfWork { get; }
        Task Create(Address address);
        Task Update(Address address);
        Task<Address> Get(Guid id);
        Task<IEnumerable<Address>> Get(Expression<Func<Address, bool>> filter);
    }
}
