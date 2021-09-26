using ECommerce.Compras.Gateway.Models;
using ECommerce.Compras.Gateway.Models.Carrinho;
using System;
using System.Threading.Tasks;

namespace ECommerce.Compras.Gateway.Interfaces
{
    public interface ICarrinhoService
    {
        // Carrinho
        Task<CarrinhoDto> BuscarCarrinho(Guid id);
        Task<ServiceResponse> AdicionarCarrinho(CarrinhoDto dto);
        Task<ServiceResponse> ExcluirCarrinho(Guid id);

        // Item carrinho
        Task<ServiceResponse> AdicionarItemCarrinho(ItemCarrinhoDto dto);
        Task<ServiceResponse> ExcluirItemCarrinho(Guid id);
    }
}
