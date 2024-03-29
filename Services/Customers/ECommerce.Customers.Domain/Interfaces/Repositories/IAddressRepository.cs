﻿using ECommerce.Customers.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ECommerce.Customers.Domain.Interfaces.Repositories
{
    public interface IAddressRepository : IDisposable
    {
        Task Create(Address address);
        Task Update(Address address);
        Task<Address> Get(Guid id);
        Task<IEnumerable<Address>> Get(Expression<Func<Address, bool>> filter);
    }
}
