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
    public class CustomerRepository : ICustomerRepository
    {
        public CustomerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        private readonly ApplicationDbContext _context;

        public IUnitOfWork UnitOfWork => _context;

        public async Task Create(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
        }

        public async Task Update(Customer customer)
        {
            _context.Customers.Update(customer);

            await Task.CompletedTask;
        }

        public async Task<Customer> Get(Guid id)
        {
            return await _context.Customers
                .Include(c => c.Document)
                .Include(c => c.Email)
                .Include(c => c.Address)
                .Include(c => c.Phone)
                .Where(c => c.Enabled == true)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Customer>> Get(int page, int rows)
        {
            return await _context.Customers
                .Include(c => c.Document)
                .Include(c => c.Email)
                .Include(c => c.Address)
                .Include(c => c.Phone)
                .AsNoTracking()
                .Where(c => c.Enabled == true)
                .ToListAsync();
        }

        public async Task<IEnumerable<Customer>> Get(Expression<Func<Customer, bool>> filter, int page, int rows)
        {
            return await _context.Customers
                .Include(c => c.Document)
                .Include(c => c.Email)
                .Include(c => c.Address)
                .Include(c => c.Phone)
                .AsNoTracking()
                .Where(c => c.Enabled == true)
                .Where(filter)
                .ToListAsync();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

        public async Task Delete(Guid id)
        {
            var customer = await Get(id);
            _context.Customers.Remove(customer);
        }

        public async Task<IEnumerable<Customer>> Get()
        {
            return await _context.Customers
                .Include(c => c.Document)
                .Include(c => c.Email)
                .Include(c => c.Address)
                .Include(c => c.Phone)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Customer>> Get(Expression<Func<Customer, bool>> filter)
        {
            return await _context.Customers
                .Include(c => c.Document)
                .Include(c => c.Email)
                .Include(c => c.Address)
                .Include(c => c.Phone)
                .Where(filter)
                .ToListAsync();
        }
    }
}
