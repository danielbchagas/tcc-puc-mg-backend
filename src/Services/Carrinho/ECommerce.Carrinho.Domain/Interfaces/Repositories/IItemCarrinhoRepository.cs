using ECommerce.Carrinho.Domain.Interfaces.Data;
using ECommerce.Carrinho.Domain.Models;
using System;
using System.Threading.Tasks;

namespace ECommerce.Carrinho.Domain.Interfaces.Repositories
{
    public interface IItemCarrinhoRepository : IDisposable
    {
        IUnitOfWork UnitOfWork { get; }
        Task<ItemCarrinho> BuscarPorProdutoId(Guid id);
        Task ExcluirPorProdutoId(Guid id);
    }
}
