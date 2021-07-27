using ECommerce.Clientes.Domain.Interfaces.Data;
using ECommerce.Clientes.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ECommerce.Clientes.Domain.Interfaces.Repositories
{
    public interface IEnderecoRepository : IDisposable
    {
        IUnitOfWork UnitOfWork { get; }
        Task Adicionar(Endereco cliente);
        Task Atualizar(Endereco cliente);
        Task Excluir(Guid id);
        Task<Endereco> Buscar(Guid id);
        Task<IEnumerable<Endereco>> Buscar(int? pagina, int? linhas);
        Task<IEnumerable<Endereco>> Buscar(Expression<Func<Endereco, bool>> filtro, int? pagina, int? linhas);
    }
}
