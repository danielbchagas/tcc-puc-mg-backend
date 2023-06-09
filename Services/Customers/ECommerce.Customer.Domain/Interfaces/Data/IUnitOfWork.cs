using System;
using System.Threading.Tasks;

namespace ECommerce.Customers.Domain.Interfaces.Data
{
    public interface IUnitOfWork : IDisposable
    {
        Task<bool> Commit();
    }
}
