using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ECommerce.Customer.Domain.Interfaces.Data;
using ECommerce.Customer.Domain.Models;

namespace ECommerce.Customer.Domain.Interfaces.Repositories
{
    public interface IEmailRepository : IDisposable
    {
        IUnitOfWork UnitOfWork { get; }
        Task Create(Email email);
        Task Update(Email email);
        Task<Email> Get(Guid id);
        Task<IEnumerable<Email>> Get(Expression<Func<Email, bool>> filter);
    }
}
