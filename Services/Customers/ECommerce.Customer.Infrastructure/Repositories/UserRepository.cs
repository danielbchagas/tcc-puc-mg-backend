using ECommerce.Customers.Domain.Interfaces.Repositories;
using ECommerce.Customers.Domain.Models;
using ECommerce.Customers.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Customers.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        public UserRepository(ApplicationDbContext context)
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
    }
}
