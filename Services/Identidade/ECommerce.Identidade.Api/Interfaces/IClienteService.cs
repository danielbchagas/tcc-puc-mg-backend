using ECommerce.Identidade.Api.Models;
using Refit;
using System.Threading.Tasks;

namespace ECommerce.Identidade.Api.Interfaces
{
    public interface IClienteService
    {
        [Post("/api/clientes")]
        Task<IApiResponse> Adicionar([Body]ClienteDto cliente, [Authorize("Bearer")] string token);
    }
}
