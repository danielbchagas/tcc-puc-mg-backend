using ECommerce.Identidade.Api.Models;
using FluentValidation.Results;
using System.Threading.Tasks;

namespace ECommerce.Identidade.Api.Interfaces
{
    public interface IClienteService
    {
        void AddToken(string token);
        Task<ValidationResult> Adicionar(ClienteDto cliente);
        Task<ValidationResult> Adicionar(DocumentoDto documento);
        Task<ValidationResult> Adicionar(TelefoneDto telefone);
        Task<ValidationResult> Adicionar(EmailDto email);
    }
}
