using ECommerce.Core.Contracts.Data;
using ECommerce.Core.Models.Customer;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ECommerce.Customer.Domain.Interfaces.Repositories
{
    public interface IDocumentRepository : IDisposable
    {
        IUnitOfWork UnitOfWork { get; }
        Task Create(Document document);
        Task Update(Document document);
        Task<Document> Get(Guid id);
        Task<IEnumerable<Document>> Get(Expression<Func<Document, bool>> filter);
    }
}
