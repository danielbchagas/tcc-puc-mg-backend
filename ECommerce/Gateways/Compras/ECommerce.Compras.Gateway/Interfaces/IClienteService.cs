using ECommerce.Compras.Gateway.Models.Cliente;
using System;
using System.Threading.Tasks;
using Refit;

namespace ECommerce.Compras.Gateway.Interfaces
{
    public interface IClienteService
    {
        [Get("/api/clientes/{id}")]
        Task<ApiResponse<ClienteDto>> BuscarCliente(Guid id);

        [Get("/api/documentos/{id}")]
        Task<ApiResponse<DocumentoDto>> BuscarDocumento(Guid id);

        [Get("/api/emails/{id}")]
        Task<ApiResponse<EmailDto>> BuscarEmail(Guid id);

        [Get("/api/telefones/{id}")]
        Task<ApiResponse<TelefoneDto>> BuscarTelefone(Guid id);

        [Get("/api/enderecos/{id}")]
        Task<ApiResponse<EnderecoDto>> BuscarEndereco(Guid id);
    }
}
