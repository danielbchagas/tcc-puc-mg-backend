using System.Threading.Tasks;

namespace ECommerce.Products.Domain.Interfaces.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
