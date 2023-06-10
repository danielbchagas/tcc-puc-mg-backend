using System;
using System.Threading.Tasks;

namespace ECommerce.Products.Domain.Interfaces.Data
{
    public interface IUnitOfWork : IDisposable
    {
        Task<bool> Commit();
    }
}
