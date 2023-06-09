using ECommerce.Customers.Domain.Interfaces.Repositories;
using ECommerce.Customers.Domain.Models;
using ECommerce.Customers.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ECommerce.Customers.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        public CustomerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        private readonly ApplicationDbContext _context;

        public async Task Create(Customer person)
        {
            await _context.Customers.AddAsync(person);
        }

        public async Task Update(Customer person)
        {
            _context.Customers.Update(person);

            await Task.CompletedTask;
        }

        public async Task<Customer> Get(Guid id)
        {
            return await _context.Customers
                .Include(c => c.Document)
                .Include(c => c.Email)
                .Include(c => c.Address)
                .Include(c => c.Phone)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task Delete(Guid id)
        {
            var customer = await Get(id);
            _context.Customers.Remove(customer);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

        public async Task<IList<Customer>> Get()
        {
            return await _context.Customers
                .Include(c => c.Document)
                .Include(c => c.Email)
                .Include(c => c.Address)
                .Include(c => c.Phone)
                .ToListAsync();
        }

        public async Task<IList<Customer>> GetData(Expression<Func<Customer, bool>> expression = null, 
            Func<IQueryable<Customer>, IIncludableQueryable<Customer, object>> includes = null, 
            int? skip = null, 
            int? take = null)
        {
            var query = _context.Customers.AsQueryable();

            if(expression != null)
                query = query.Where(expression);

            if(includes != null)
                query = includes(query);

            if(skip.HasValue)
                query = query.Skip(skip.Value);

            if(take.HasValue)
                query = query.Take(take.Value);

            return await query.ToListAsync();
        }
    }
}
