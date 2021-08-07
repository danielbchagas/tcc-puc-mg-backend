using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ECommerce.Cliente.Domain.Interfaces.Data;

namespace ECommerce.Cliente.Domain.Interfaces.Repositories
{
    public interface IClienteRepository : IDisposable
    {
        IUnitOfWork UnitOfWork { get; }
        Task Adicionar(Models.Cliente cliente);
        Task Atualizar(Models.Cliente cliente);
        Task Excluir(Guid id);
        Task<Models.Cliente> Buscar(Guid id);
        Task<IEnumerable<Models.Cliente>> Buscar();
        Task<IEnumerable<Models.Cliente>> Buscar(int pagina, int linhas);
        Task<IEnumerable<Models.Cliente>> Buscar(Expression<Func<Models.Cliente, bool>> filtro, int pagina, int linhas);
    }
}
