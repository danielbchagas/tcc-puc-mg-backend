using System.Threading.Tasks;

namespace ECommerce.Core.Contracts.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
