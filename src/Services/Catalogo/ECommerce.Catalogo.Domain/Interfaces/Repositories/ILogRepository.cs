using System.Threading.Tasks;
using ECommerce.Catalogo.Domain.Models;

namespace ECommerce.Catalogo.Domain.Interfaces.Repositories
{
    public interface ILogRepository
    {
        Task<bool> Adicionar(LogEvento log);
    }
}
