using ECommerce.Carrinho.Api.Interfaces.Data;
using ECommerce.Carrinho.Api.Models;
using System;
using System.Threading.Tasks;

namespace ECommerce.Carrinho.Api.Interfaces.Repositories
{
    public interface ICarrinhoRepository : IDisposable
    {
        IUnitOfWork UnitOfWork { get; }
        Task<Models.Carrinho> BuscarPorId(Guid id);
        Task Adicionar(Models.Carrinho carrinho);
        Task Atualizar(Models.Carrinho carrinho);
    }
}
