using ECommerce.Clientes.Domain.Models;
using System.Threading.Tasks;

namespace ECommerce.Clientes.Domain.Interfaces.Repositories
{
    public interface ILogRepository
    {
        Task<bool> Adicionar(LogEvento log);
    }
}
