using ECommerce.Customers.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ECommerce.Customers.Domain.Interfaces.Repositories
{
    public interface IEmailRepository : IDisposable
    {
        Task Create(Email email);
        Task Update(Email email);
        Task<Email> Get(Guid id);
        Task<IEnumerable<Email>> Get(Expression<Func<Email, bool>> filter);
    }
}
