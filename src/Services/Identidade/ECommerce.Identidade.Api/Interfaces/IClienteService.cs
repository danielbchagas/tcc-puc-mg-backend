using ECommerce.Identidade.Api.Models;
using FluentValidation.Results;
using System.Threading.Tasks;
using Refit;

namespace ECommerce.Identidade.Api.Interfaces
{
    public interface IClienteService
    {
        [Post("/api/clientes")]
        Task<ApiResponse<string>> Adicionar([Body]ClienteDto cliente, [Authorize("Bearer")] string token);
    }
}
