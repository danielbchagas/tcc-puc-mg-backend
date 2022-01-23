using ECommerce.Compras.Gateway.Models.Catalogo;
using Refit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Compras.Gateway.Interfaces
{
    public interface ICatalogoService
    {
        [Get("/api/produtos/{id}")]
        Task<ApiResponse<ProdutoDto>> Buscar(Guid id);

        [Get("/api/produtos/{pagina}/{linhas}")]
        Task<ApiResponse<IEnumerable<ProdutoDto>>> Buscar(int pagina, int linhas);

        [Get("/api/produtos/{nome}/{pagina}/{linhas}")]
        Task<ApiResponse<IEnumerable<ProdutoDto>>> Buscar(string nome, int pagina, int linhas);

        [Put("/api/produtos")]
        Task<ApiResponse<string>> Atualizar(ProdutoDto produto, [Authorize("Bearer")] string token);

        [Post("/api/produtos")]
        Task<ApiResponse<string>> Cadastrar(ProdutoDto produto, [Authorize("Bearer")] string token);
    }
}
