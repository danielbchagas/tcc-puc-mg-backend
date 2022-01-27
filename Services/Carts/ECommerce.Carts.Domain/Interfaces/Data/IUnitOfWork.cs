using System.Threading.Tasks;

namespace ECommerce.Carts.Domain.Interfaces.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
