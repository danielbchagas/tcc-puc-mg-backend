using System.Threading.Tasks;

namespace ECommerce.Customer.Domain.Interfaces.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
