using ECommerce.Compras.Gateway.Models.Cliente;
using FluentValidation.Results;
using System;
using System.Threading.Tasks;

namespace ECommerce.Compras.Gateway.Interfaces
{
    public interface IClienteService
    {
        Task<ValidationResult> Desativar(Guid id);
        Task<ClienteDto> Buscar(Guid id);
    }
}
