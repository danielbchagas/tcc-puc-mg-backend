using ECommerce.Compras.Gateway.Dtos.Carrinho;
using ECommerce.Compras.Gateway.Models;
using ECommerce.Compras.Gateway.Models.Carrinho;
using System.Threading.Tasks;

namespace ECommerce.Compras.Gateway.Interfaces
{
    public interface ICarrinhoService
    {
        // Carrinho
        Task<Carrinho> Buscar(BuscarCarrinhoPorClienteDto dto);
        Task<ServiceResponse> Adicionar(AdicionarCarrinhoDto dto);
        Task<ServiceResponse> Excluir(ExcluirCarrinhoDto dto);

        // Item carrinho
        Task<ServiceResponse> Adicionar(AdicionarItemCarrinhoDto dto);
        Task<ServiceResponse> Excluir(ExcluirItemCarrinhoDto dto);
    }
}
