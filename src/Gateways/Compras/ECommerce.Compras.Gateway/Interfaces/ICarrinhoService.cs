using ECommerce.Compras.Gateway.Dtos.Carrinho;
using ECommerce.Compras.Gateway.Models;
using ECommerce.Compras.Gateway.Models.Carrinho;
using System;
using System.Threading.Tasks;

namespace ECommerce.Compras.Gateway.Interfaces
{
    public interface ICarrinhoService
    {
        // Carrinho
        Task<Carrinho> BuscarCarrinho(Guid id);
        Task<ServiceResponse> AdicionarCarrinho(AdicionarCarrinhoDto dto);
        Task<ServiceResponse> ExcluirCarrinho(Guid id);

        // Item carrinho
        Task<ServiceResponse> AdicionarItemCarrinho(AdicionarItemCarrinhoDto dto);
        Task<ServiceResponse> ExcluirItemCarrinho(Guid id);
    }
}
