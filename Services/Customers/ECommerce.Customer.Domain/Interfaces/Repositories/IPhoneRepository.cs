using ECommerce.Customers.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ECommerce.Customers.Domain.Interfaces.Repositories
{
    public interface IPhoneRepository : IDisposable
    {
        Task Create(Phone phone);
        Task Update(Phone phone);
        Task<Phone> Get(Guid id);
        Task<IEnumerable<Phone>> Get(Expression<Func<Phone, bool>> filter);
    }
}
