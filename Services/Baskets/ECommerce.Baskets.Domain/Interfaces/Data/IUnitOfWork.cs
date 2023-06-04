using Microsoft.EntityFrameworkCore.Storage;
using System.Threading.Tasks;

namespace ECommerce.Baskets.Domain.Interfaces.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
        Task<IDbContextTransaction> OpenTransaction();
        Task CommitTransaction();
        Task RollbackTransaction();
    }
}
