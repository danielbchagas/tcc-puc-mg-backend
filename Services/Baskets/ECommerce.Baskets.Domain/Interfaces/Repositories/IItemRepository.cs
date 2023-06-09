using ECommerce.Baskets.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Baskets.Domain.Interfaces.Repositories
{
    public interface IItemRepository : IDisposable
    {
        Task Create(IEnumerable<Item> item);
        Task Update(IEnumerable<Item> item);
    }
}
