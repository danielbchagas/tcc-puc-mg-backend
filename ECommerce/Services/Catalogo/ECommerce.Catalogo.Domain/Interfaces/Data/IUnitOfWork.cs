using System.Threading.Tasks;

namespace ECommerce.Catalogo.Domain.Interfaces.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
