using System;
using System.Threading.Tasks;
using ECommerce.Cliente.Domain.Interfaces.Data;
using ECommerce.Cliente.Domain.Models;

namespace ECommerce.Cliente.Domain.Interfaces.Repositories
{
    public interface ILogEventoRepository : IDisposable
    {
        IUnitOfWork UnitOfWork { get; }
        Task Adicionar(LogEvento log);
    }
}
