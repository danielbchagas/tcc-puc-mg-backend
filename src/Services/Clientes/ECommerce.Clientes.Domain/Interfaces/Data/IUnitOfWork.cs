using System.Threading.Tasks;

namespace ECommerce.Clientes.Domain.Interfaces.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
