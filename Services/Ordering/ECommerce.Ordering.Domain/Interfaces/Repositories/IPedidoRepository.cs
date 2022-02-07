using ECommerce.Core.Models.Ordering;
using ECommerce.Ordering.Domain.Interfaces.Data;
using System;
using System.Threading.Tasks;

namespace ECommerce.Ordering.Domain.Interfaces.Repositories
{
    public interface IPedidoRepository
    {
        IUnitOfWork UnitOfWork { get; }

        Task<Order> Get(Guid id);
        Task Create(Order order);
        Task Update(Order order);
    }
}
