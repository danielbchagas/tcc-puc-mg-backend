using System;
using System.Threading.Tasks;
using ECommerce.Cliente.Domain.Interfaces.Data;
using ECommerce.Cliente.Domain.Models;

namespace ECommerce.Cliente.Domain.Interfaces.Repositories
{
    public interface IEnderecoRepository : IDisposable
    {
        IUnitOfWork UnitOfWork { get; }
        Task Adicionar(Endereco cliente);
        Task Atualizar(Endereco cliente);
        Task<Endereco> Buscar(Guid id);
    }
}
