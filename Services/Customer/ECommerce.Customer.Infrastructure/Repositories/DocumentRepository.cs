using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ECommerce.Customer.Domain.Interfaces.Data;
using ECommerce.Customer.Domain.Interfaces.Repositories;
using ECommerce.Customer.Domain.Models;
using ECommerce.Customer.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Customer.Infrastructure.Repositories
{
    public class DocumentRepository : IDocumentRepository
    {
        public DocumentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        private readonly ApplicationDbContext _context;

        public IUnitOfWork UnitOfWork => _context;

        public async Task Create(Document document)
        {
            await _context.Documents.AddAsync(document);
        }

        public async Task Update(Document document)
        {
            _context.Documents.Update(document);
            await Task.CompletedTask;
        }

        public async Task<Document> Get(Guid id)
        {
            return await _context.Documents
                .Include(d => d.Customer)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

        public async Task<IEnumerable<Document>> Get(Expression<Func<Document, bool>> filter)
        {
            return await _context.Documents
                .Include(d => d.Customer)
                .Where(filter)
                .ToListAsync();
        }
    }
}
