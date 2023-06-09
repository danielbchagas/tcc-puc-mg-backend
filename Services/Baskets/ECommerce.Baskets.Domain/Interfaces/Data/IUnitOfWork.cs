using System;
using System.Threading.Tasks;

namespace ECommerce.Baskets.Domain.Interfaces.Data
{
    public interface IUnitOfWork : IDisposable
    {
        Task<bool> Commit();
    }
}
