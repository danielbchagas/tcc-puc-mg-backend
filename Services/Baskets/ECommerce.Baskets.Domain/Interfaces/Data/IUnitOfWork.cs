using System.Threading.Tasks;

namespace ECommerce.Basket.Domain.Interfaces.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
