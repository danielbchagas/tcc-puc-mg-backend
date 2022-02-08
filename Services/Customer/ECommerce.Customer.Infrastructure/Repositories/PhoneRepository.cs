﻿using ECommerce.Core.Contracts.Data;
using ECommerce.Core.Models.Customer;
using ECommerce.Customer.Domain.Interfaces.Repositories;
using ECommerce.Customer.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ECommerce.Customer.Infrastructure.Repositories
{
    public class PhoneRepository : IPhoneRepository
    {
        public PhoneRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        private readonly ApplicationDbContext _context;
        public IUnitOfWork UnitOfWork => _context;

        public async Task Create(Phone phone)
        {
            await _context.Phones.AddAsync(phone);
        }

        public Task Update(Phone phone)
        {
            _context.Phones.Update(phone);
            return Task.CompletedTask;
        }

        public async Task<Phone> Get(Guid id)
        {
            return await _context.Phones
                .Include(t => t.Customer)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

        public async Task<IEnumerable<Phone>> Get(Expression<Func<Phone, bool>> filter)
        {
            return await _context.Phones
                .Include(t => t.Customer)
                .Where(filter)
                .ToListAsync();
        }
    }
}
