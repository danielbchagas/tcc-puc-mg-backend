using ECommerce.Compras.Gateway.Models;
using ECommerce.Compras.Gateway.Models.Catalogo;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Compras.Gateway.Interfaces
{
    public interface ICatalogoService
    {
        Task<ProdutoDto> Buscar(Guid id);
        Task<IEnumerable<ProdutoDto>> Buscar(int pagina, int linhas);
        Task<IEnumerable<ProdutoDto>> Buscar(string nome, int pagina, int linhas);
        Task<ServiceResponse> Atualizar(ProdutoDto produto);
        Task<ServiceResponse> Cadastrar(ProdutoDto produto);
    }
}
