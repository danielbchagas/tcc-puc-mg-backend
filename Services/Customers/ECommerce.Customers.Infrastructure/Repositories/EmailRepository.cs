using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ECommerce.Customers.Domain.Interfaces.Data;
using ECommerce.Customers.Domain.Interfaces.Repositories;
using ECommerce.Customers.Domain.Models;
using ECommerce.Customers.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Customers.Infrastructure.Repositories
{
    public class EmailRepository : IEmailRepository
    {
        public EmailRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        private readonly ApplicationDbContext _context;

        public IUnitOfWork UnitOfWork => _context;

        public async Task Create(Email email)
        {
            await _context.Emails.AddAsync(email);
        }

        public async Task Update(Email email)
        {
            _context.Emails.Update(email);
            await Task.CompletedTask;
        }

        public async Task<Email> Get(Guid id)
        {
            return await _context.Emails
                .Include(e => e.Customer)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

        public async Task<IEnumerable<Email>> Get(Expression<Func<Email, bool>> filter)
        {
            return await _context.Emails
                .Include(e => e.Customer)
                .Where(filter)
                .ToListAsync();
        }
    }
}
