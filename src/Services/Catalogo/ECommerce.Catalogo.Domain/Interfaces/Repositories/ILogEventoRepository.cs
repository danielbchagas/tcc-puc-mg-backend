using System.Threading.Tasks;
using ECommerce.Catalogo.Domain.Interfaces.Data;
using ECommerce.Catalogo.Domain.Models;

namespace ECommerce.Catalogo.Domain.Interfaces.Repositories
{
    public interface ILogEventoRepository
    {
        IUnitOfWork UnitOfWork { get; }
        Task Adicionar(LogEvento log);
    }
}
