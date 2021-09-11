using ECommerce.Identidade.Api.Models;
using FluentValidation.Results;
using System.Threading.Tasks;

namespace ECommerce.Identidade.Api.Interfaces
{
    public interface IClienteService
    {
        void AddToken(string token);
        Task<ValidationResult> Novo(ClienteDto cliente);
        Task<ValidationResult> Novo(DocumentoDto documento);
        Task<ValidationResult> Novo(TelefoneDto telefone);
        Task<ValidationResult> Novo(EmailDto email);
    }
}
