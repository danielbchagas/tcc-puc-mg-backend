using System.Threading.Tasks;

namespace ECommerce.Catalog.Domain.Interfaces.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
