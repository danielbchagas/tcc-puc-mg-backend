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
    public class AddressRepository : IAddressRepository
    {
        public AddressRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        private readonly ApplicationDbContext _context;

        public IUnitOfWork UnitOfWork => _context;

        public async Task Create(Address address)
        {
            await _context.Addresses.AddAsync(address);
        }

        public async Task Update(Address address)
        {
            _context.Addresses.Update(address);

            await Task.CompletedTask;
        }

        public async Task<Address> Get(Guid id)
        {
            return await _context.Addresses
                .Include(e => e.User)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

        public async Task<IEnumerable<Address>> Get(Expression<Func<Address, bool>> filter)
        {
            return await _context.Addresses
                .Include(e => e.User)
                .Where(filter)
                .ToListAsync();
        }
    }
}
