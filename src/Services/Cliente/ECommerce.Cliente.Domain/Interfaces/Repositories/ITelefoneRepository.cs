using ECommerce.Cliente.Domain.Interfaces.Data;
using ECommerce.Cliente.Domain.Models;
using System;
using System.Threading.Tasks;

namespace ECommerce.Cliente.Domain.Interfaces.Repositories
{
    public interface ITelefoneRepository : IDisposable
    {
        IUnitOfWork UnitOfWork { get; }
        Task Adicionar(Telefone telefone);
        Task Atualizar(Telefone telefone);
        Task<Telefone> Buscar(Guid id);
    }
}
