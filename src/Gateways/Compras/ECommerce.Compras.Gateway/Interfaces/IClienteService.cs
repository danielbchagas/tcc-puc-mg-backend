using ECommerce.Compras.Gateway.Models.Cliente;
using System;
using System.Threading.Tasks;

namespace ECommerce.Compras.Gateway.Interfaces
{
    public interface IClienteService
    {
        Task<ClienteResponseMessage> Atualizar(ClienteDto cliente);
        Task<ClienteResponseMessage> Desativar(Guid id);
    }
}
