using ECommerce.Produtos.Domain.Interfaces.Data;
using ECommerce.Produtos.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ECommerce.Produtos.Domain.Interfaces.Repositories
{
    public interface IProdutoRepository : IDisposable
    {
        IUnitOfWork UnitOfWork { get; }
        Task Adicionar(Produto produto);
        Task Atualizar(Produto produto);
        Task Excluir(Guid id);
        Task<Produto> Buscar(Guid id);
        Task<IEnumerable<Produto>> Buscar(int? pagina, int? linhas);
        Task<IEnumerable<Produto>> Buscar(Expression<Func<Produto, bool>> filtro, int? pagina, int? linhas);
    }
}
