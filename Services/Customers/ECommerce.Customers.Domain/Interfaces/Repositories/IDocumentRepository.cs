﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ECommerce.Customers.Domain.Interfaces.Data;
using ECommerce.Customers.Domain.Models;

namespace ECommerce.Customers.Domain.Interfaces.Repositories
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
