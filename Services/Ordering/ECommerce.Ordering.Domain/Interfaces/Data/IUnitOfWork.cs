using System.Threading.Tasks;

namespace ECommerce.Ordering.Domain.Interfaces.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
