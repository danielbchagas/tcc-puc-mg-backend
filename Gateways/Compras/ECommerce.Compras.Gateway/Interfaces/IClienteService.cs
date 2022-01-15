using ECommerce.Compras.Gateway.Models.Cliente;
using System;
using System.Threading.Tasks;
using Refit;

namespace ECommerce.Compras.Gateway.Interfaces
{
    public interface IClienteService
    {
        [Get("/api/clientes/{id}")]
        Task<ApiResponse<ClienteDto>> BuscarCliente(Guid id, [Authorize("Bearer")] string token);

        [Get("/api/documentos/{id}")]
        Task<ApiResponse<DocumentoDto>> BuscarDocumento(Guid id, [Authorize("Bearer")] string token);

        [Get("/api/emails/{id}")]
        Task<ApiResponse<EmailDto>> BuscarEmail(Guid id, [Authorize("Bearer")] string token);

        [Get("/api/telefones/{id}")]
        Task<ApiResponse<TelefoneDto>> BuscarTelefone(Guid id, [Authorize("Bearer")] string token);

        [Get("/api/enderecos/{id}")]
        Task<ApiResponse<EnderecoDto>> BuscarEndereco(Guid id, [Authorize("Bearer")] string token);
    }
}
