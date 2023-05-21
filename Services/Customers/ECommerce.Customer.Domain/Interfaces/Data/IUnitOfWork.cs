using System.Threading.Tasks;

namespace ECommerce.Customers.Domain.Interfaces.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
