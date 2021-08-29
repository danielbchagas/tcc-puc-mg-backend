using System.Threading.Tasks;

namespace ECommerce.Carrinho.Api.Interfaces.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
