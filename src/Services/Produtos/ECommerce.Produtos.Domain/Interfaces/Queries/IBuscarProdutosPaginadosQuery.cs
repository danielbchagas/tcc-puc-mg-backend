using ECommerce.Produtos.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Produtos.Domain.Interfaces.Queries
{
    public interface IBuscarProdutosPaginadosQuery
    {
        Task<IEnumerable<Produto>> Buscar(int? pagina, int? linhas);
    }
}
