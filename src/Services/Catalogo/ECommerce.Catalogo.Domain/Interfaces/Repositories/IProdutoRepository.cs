using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ECommerce.Catalogo.Domain.Interfaces.Data;
using ECommerce.Catalogo.Domain.Models;

namespace ECommerce.Catalogo.Domain.Interfaces.Repositories
{
    public interface IProdutoRepository : IDisposable
    {
        IUnitOfWork UnitOfWork { get; }
        Task Adicionar(Produto produto);
        Task Atualizar(Produto produto);
        Task Excluir(Guid id);
        Task<Produto> Buscar(Guid id);
        Task<IEnumerable<Produto>> Buscar();
        Task<IEnumerable<Produto>> Buscar(int pagina, int linhas);
        Task<IEnumerable<Produto>> Buscar(Expression<Func<Produto, bool>> filtro, int pagina, int linhas);
    }
}
