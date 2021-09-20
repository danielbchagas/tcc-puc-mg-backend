using ECommerce.Carrinho.Domain.Interfaces.Data;
using System;
using System.Threading.Tasks;

namespace ECommerce.Carrinho.Domain.Interfaces.Repositories
{
    public interface ICarrinhoRepository : IDisposable
    {
        IUnitOfWork UnitOfWork { get; }
        Task<Models.Carrinho> BuscarPorClienteId(Guid clienteId);
        Task ExcluirPorClienteId(Guid clienteId);
        Task Adicionar(Models.Carrinho carrinho);
        Task Atualizar(Models.Carrinho carrinho);
    }
}
