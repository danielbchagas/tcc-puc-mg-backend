using ECommerce.Carrinho.Api.Interfaces.Data;
using ECommerce.Carrinho.Api.Models;
using System;
using System.Threading.Tasks;

namespace ECommerce.Carrinho.Api.Interfaces.Repositories
{
    public interface ICarrinhoClienteRepository : IDisposable
    {
        IUnitOfWork UnitOfWork { get; }
        Task<CarrinhoCliente> BuscarPorId(Guid id);
        Task Adicionar(CarrinhoCliente carrinho);
        Task Atualizar(CarrinhoCliente carrinho);
    }
}
