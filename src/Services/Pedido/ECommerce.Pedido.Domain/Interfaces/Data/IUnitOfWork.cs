using System.Threading.Tasks;

namespace ECommerce.Pedido.Domain.Interfaces.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
