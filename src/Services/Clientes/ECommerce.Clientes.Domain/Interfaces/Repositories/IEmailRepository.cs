using ECommerce.Clientes.Domain.Interfaces.Data;
using ECommerce.Clientes.Domain.Models;
using System;
using System.Threading.Tasks;

namespace ECommerce.Clientes.Domain.Interfaces.Repositories
{
    public interface IEmailRepository : IDisposable
    {
        IUnitOfWork UnitOfWork { get; }
        Task Adicionar(Email email);
        Task Atualizar(Email email);
        Task<Email> Buscar(Guid id);
    }
}
