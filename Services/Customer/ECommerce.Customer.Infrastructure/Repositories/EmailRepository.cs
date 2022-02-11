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
                .Include(e => e.User)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

        public async Task<IEnumerable<Email>> Get(Expression<Func<Email, bool>> filter)
        {
            return await _context.Emails
                .Include(e => e.User)
                .Where(filter)
                .ToListAsync();
        }
    }
}
