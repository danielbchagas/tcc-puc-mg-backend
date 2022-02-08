using ECommerce.Core.Contracts.Data;
using ECommerce.Core.Models.Customer;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

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
