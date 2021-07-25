using ECommerce.Produtos.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ECommerce.Produtos.Domain.Interfaces.Queries
{
    public interface IProdutoQuery
    {
        Task<Produto> Buscar(Guid id);
        Task<IEnumerable<Produto>> Buscar(int? pagina, int? linhas);
        Task<IEnumerable<Produto>> Buscar(Expression<Func<Produto, bool>> filtro, int? pagina, int? linhas);
    }
}
