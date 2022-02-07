using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ECommerce.Customer.Domain.Interfaces.Data;
using ECommerce.Customer.Domain.Models;

namespace ECommerce.Customer.Domain.Interfaces.Repositories
{
    public interface IPhoneRepository : IDisposable
    {
        IUnitOfWork UnitOfWork { get; }
        Task Create(Phone phone);
        Task Update(Phone phone);
        Task<Phone> Get(Guid id);
        Task<IEnumerable<Phone>> Get(Expression<Func<Phone, bool>> filter);
    }
}
