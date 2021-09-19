using ECommerce.Compras.Gateway.Models;
using ECommerce.Compras.Gateway.Models.Carrinho;
using System;
using System.Threading.Tasks;

namespace ECommerce.Compras.Gateway.Interfaces
{
    public interface ICarrinhoService
    {
        Task<ServiceResponse> Atualizar(ItemCarrinho item);
        Task<Carrinho> Buscar();
        Task Excluir(Guid produtoId);
    }
}
