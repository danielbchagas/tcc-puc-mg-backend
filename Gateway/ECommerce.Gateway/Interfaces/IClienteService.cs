using ECommerce.Compras.Gateway.Models.Cliente;
using System;
using System.Threading.Tasks;
using Refit;

namespace ECommerce.Compras.Gateway.Interfaces
{
    public interface IClienteService
    {
        [Get("/api/clientes/{id}")]
        Task<ApiResponse<ClienteDto>> GetCliente(Guid id, [Authorize("Bearer")] string token);

        [Put("/api/clientes")]
        Task<IApiResponse> UpdateCliente(ClienteDto cliente, [Authorize("Bearer")] string token);

        [Delete("/api/clientes/{id}")]
        Task<IApiResponse> DeleteCliente(Guid id, [Authorize("Bearer")] string token);

        [Get("/api/documentos/{id}")]
        Task<ApiResponse<DocumentoDto>> GetDocumento(Guid id, [Authorize("Bearer")] string token);

        [Get("/api/emails/{id}")]
        Task<ApiResponse<EmailDto>> GetEmail(Guid id, [Authorize("Bearer")] string token);

        [Get("/api/telefones/{id}")]
        Task<ApiResponse<TelefoneDto>> GetTelefone(Guid id, [Authorize("Bearer")] string token);

        [Get("/api/enderecos/{id}")]
        Task<ApiResponse<EnderecoDto>> GetEndereco(Guid id, [Authorize("Bearer")] string token);
    }
}
