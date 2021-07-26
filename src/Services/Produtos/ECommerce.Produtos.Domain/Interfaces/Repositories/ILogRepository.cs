using ECommerce.Produtos.Domain.Models;
using System.Threading.Tasks;

namespace ECommerce.Produtos.Domain.Interfaces.Repositories
{
    public interface ILogRepository
    {
        Task<bool> Adicionar(LogEvento log);
    }
}
