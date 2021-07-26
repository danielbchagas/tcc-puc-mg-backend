using ECommerce.Produtos.Domain.Models;
using System;
using System.Threading.Tasks;

namespace ECommerce.Produtos.Domain.Interfaces.Queries
{
    public interface IBuscarProdutoPorIdQuery
    {
        Task<Produto> Buscar(Guid id);
    }
}
