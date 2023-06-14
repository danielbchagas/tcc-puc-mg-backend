using ECommerce.Customers.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ECommerce.Customers.Domain.Interfaces.Repositories
{
    public interface IDocumentRepository : IDisposable
    {
        Task Create(Document document);
        Task Update(Document document);
        Task<Document> Get(Guid id);
        Task<IEnumerable<Document>> Get(Expression<Func<Document, bool>> filter);
    }
}
