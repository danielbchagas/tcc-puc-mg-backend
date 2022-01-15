using System.Threading.Tasks;

namespace ECommerce.Cliente.Domain.Interfaces.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
