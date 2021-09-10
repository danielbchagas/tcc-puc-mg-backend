using ECommerce.Compras.Gateway.Models.Catalogo;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Compras.Gateway.Interfaces
{
    public interface ICatalogoService
    {
        Task<BuscarProdutoDto> Buscar(Guid id);
        Task<IEnumerable<BuscarProdutoDto>> Buscar(int pagina, int linhas);
        Task<IEnumerable<BuscarProdutoDto>> Buscar(string nome, int pagina, int linhas);
        Task<ValidationResult> Atualizar(AtualizarProdutoDto produto);
        Task<ValidationResult> Cadastrar(CadastrarProdutoDto produto);
    }
}
