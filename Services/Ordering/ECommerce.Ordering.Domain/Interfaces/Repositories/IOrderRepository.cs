using System;
using System.Threading.Tasks;
using ECommerce.Ordering.Domain.Interfaces.Data;
using ECommerce.Ordering.Domain.Models;

namespace ECommerce.Ordering.Domain.Interfaces.Repositories
{
    public interface IOrderRepository
    {
        IUnitOfWork UnitOfWork { get; }

        Task<Order> Buscar(Guid id);
        Task Create(Order order);
        Task Update(Order order);
    }
}
