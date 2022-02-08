﻿using ECommerce.Core.Contracts.Data;
using ECommerce.Core.Models.Ordering;
using System;
using System.Threading.Tasks;

namespace ECommerce.Ordering.Domain.Interfaces.Repositories
{
    public interface IOrderRepository
    {
        IUnitOfWork UnitOfWork { get; }

        Task<Order> Get(Guid id);
        Task Create(Order order);
        Task Update(Order order);
    }
}
