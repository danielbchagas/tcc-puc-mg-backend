using ECommerce.Carrinho.Domain.Interfaces.Data;
using ECommerce.Carrinho.Domain.Models;
using System;
using System.Threading.Tasks;

namespace ECommerce.Carrinho.Domain.Interfaces.Repositories
{
    public interface IItemCarrinhoRepository : IDisposable
    {
        IUnitOfWork UnitOfWork { get; }
        
        Task Excluir(Guid id);
        Task Adicionar(ItemCarrinho item);
        Task Atualizar(ItemCarrinho item);

        Task<ItemCarrinho> BuscarPorProdutoId(Guid produtoId);
    }
}
