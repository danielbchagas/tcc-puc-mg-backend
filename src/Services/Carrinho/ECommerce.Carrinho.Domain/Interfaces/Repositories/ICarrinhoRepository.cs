using ECommerce.Carrinho.Domain.Interfaces.Data;
using System;
using System.Threading.Tasks;
using CarrinhoCliente = ECommerce.Carrinho.Domain.Models.Carrinho;

namespace ECommerce.Carrinho.Domain.Interfaces.Repositories
{
    public interface ICarrinhoRepository : IDisposable
    {
        IUnitOfWork UnitOfWork { get; }

        Task<CarrinhoCliente> Buscar(Guid id);
        Task Adicionar(CarrinhoCliente carrinho);
        Task Atualizar(CarrinhoCliente carrinho);
        Task Excluir(Guid id);
        
        Task<CarrinhoCliente> BuscarPorClienteId(Guid clienteId);
    }
}
