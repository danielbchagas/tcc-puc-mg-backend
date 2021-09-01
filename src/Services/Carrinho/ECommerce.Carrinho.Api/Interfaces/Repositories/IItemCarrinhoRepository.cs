using ECommerce.Carrinho.Api.Interfaces.Data;
using ECommerce.Carrinho.Api.Models;
using System;
using System.Threading.Tasks;

namespace ECommerce.Carrinho.Api.Interfaces.Repositories
{
    public interface IItemCarrinhoRepository : IDisposable
    {
        IUnitOfWork UnitOfWork { get; }
        Task<ItemCarrinho> BuscarPorProdutoId(Guid id);
        Task ExcluirPorProdutoId(Guid id);
    }
}
