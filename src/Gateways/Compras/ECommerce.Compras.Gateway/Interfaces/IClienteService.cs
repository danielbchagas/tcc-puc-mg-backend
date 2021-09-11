using ECommerce.Compras.Gateway.Models;
using ECommerce.Compras.Gateway.Models.Cliente;
using System;
using System.Threading.Tasks;

namespace ECommerce.Compras.Gateway.Interfaces
{
    public interface IClienteService
    {
        Task<ServiceResponse> AtualizarCliente(ClienteDto cliente);
        Task<ClienteDto> BuscarCliente(Guid id);
        Task<ServiceResponse> DesativarCliente(Guid id);

        Task<ServiceResponse> AtualizarDocumento(DocumentoDto documento);
        Task<DocumentoDto> BuscarDocumento(Guid id);

        Task<ServiceResponse> AtualizarEmail(EmailDto email);
        Task<EmailDto> BuscarEmail(Guid id);

        Task<ServiceResponse> AtualizarTelefone(TelefoneDto telefone);
        Task<TelefoneDto> BuscarTelefone(Guid id);

        Task<ServiceResponse> AtualizarEndereco(EnderecoDto endereco);
        Task<EnderecoDto> BuscarEndereco(Guid id);
    }
}
