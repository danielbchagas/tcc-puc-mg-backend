using ECommerce.Compras.Gateway.Models.Cliente;
using System;
using System.Threading.Tasks;

namespace ECommerce.Compras.Gateway.Interfaces
{
    public interface IClienteService
    {
        Task<ClienteDto> BuscarCliente(Guid id);
        Task<DocumentoDto> BuscarDocumento(Guid id);
        Task<EmailDto> BuscarEmail(Guid id);
        Task<TelefoneDto> BuscarTelefone(Guid id);
        Task<EnderecoDto> BuscarEndereco(Guid id);
    }
}
