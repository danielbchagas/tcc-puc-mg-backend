using ECommerce.Compras.Gateway.Models.Cliente;
using FluentValidation.Results;
using System;
using System.Threading.Tasks;

namespace ECommerce.Compras.Gateway.Interfaces
{
    public interface IClienteService
    {
        Task<ValidationResult> AtualizarCliente(ClienteDto cliente);
        Task<ValidationResult> DesativarCliente(Guid id);
        Task<ClienteDto> BuscarCliente(Guid id);

        Task<ValidationResult> AdicionarDocumento(DocumentoDto documento);
        Task<ValidationResult> AtualizarDocumento(DocumentoDto documento);
        Task<DocumentoDto> BuscarDocumento(Guid id);

        Task<ValidationResult> AdicionarEmail(EmailDto email);
        Task<ValidationResult> AtualizarEmail(EmailDto email);
        Task<EmailDto> BuscarEmail(Guid id);

        Task<ValidationResult> AdicionarTelefone(TelefoneDto telefone);
        Task<ValidationResult> AtualizarTelefone(TelefoneDto telefone);
        Task<TelefoneDto> BuscarTelefone(Guid id);

        Task<ValidationResult> AdicionarEndereco(EnderecoDto endereco);
        Task<ValidationResult> AtualizarEndereco(EnderecoDto endereco);
        Task<EnderecoDto> BuscarEndereco(Guid id);
    }
}
