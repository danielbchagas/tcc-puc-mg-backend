using ECommerce.Clientes.Domain.Interfaces.Data;
using ECommerce.Clientes.Domain.Models;
using System;
using System.Threading.Tasks;

namespace ECommerce.Clientes.Domain.Interfaces.Repositories
{
    public interface IEnderecoRepository : IDisposable
    {
        IUnitOfWork UnitOfWork { get; }
        Task Adicionar(Endereco cliente);
        Task Atualizar(Endereco cliente);
        Task<Endereco> Buscar(Guid id);
    }
}
