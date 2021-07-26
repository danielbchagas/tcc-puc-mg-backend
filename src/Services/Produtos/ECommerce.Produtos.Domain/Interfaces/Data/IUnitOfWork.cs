using System.Threading.Tasks;

namespace ECommerce.Produtos.Domain.Interfaces.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
