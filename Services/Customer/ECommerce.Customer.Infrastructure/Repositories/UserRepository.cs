﻿using ECommerce.Core.Contracts.Data;
using ECommerce.Core.Models.Customer;
using ECommerce.Customer.Domain.Interfaces.Repositories;
using ECommerce.Customer.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Customer.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        private readonly ApplicationDbContext _context;

        public IUnitOfWork UnitOfWork => _context;

        public async Task Create(User person)
        {
            await _context.Users.AddAsync(person);
        }

        public async Task Update(User person)
        {
            _context.Users.Update(person);

            await Task.CompletedTask;
        }

        public async Task<User> Get(Guid id)
        {
            return await _context.Users
                .Include(c => c.Document)
                .Include(c => c.Email)
                .Include(c => c.Address)
                .Include(c => c.Phone)
                .Where(c => c.Enabled == true)
                .FirstOrDefaultAsync(c => c.Id == id);
        }
        
        public async Task Delete(Guid id)
        {
            var customer = await Get(id);
            _context.Users.Remove(customer);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
