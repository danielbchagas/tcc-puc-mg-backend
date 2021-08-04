using ECommerce.Clientes.Domain.Interfaces.Data;
using ECommerce.Clientes.Domain.Models;
using System;
using System.Threading.Tasks;

namespace ECommerce.Clientes.Domain.Interfaces.Repositories
{
    public interface ILogRepository : IDisposable
    {
        IUnitOfWork UnitOfWork { get; }
        Task Adicionar(LogEvento log);
    }
}
