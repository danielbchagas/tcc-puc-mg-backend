using System.Threading.Tasks;

namespace ECommerce.Carrinho.Domain.Interfaces.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
