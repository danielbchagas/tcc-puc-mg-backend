using ECommerce.Identidade.Api.Models;
using System.Threading.Tasks;

namespace ECommerce.Identidade.Api.Interfaces
{
    public interface IClienteService
    {
        void AddToken(string token);
        Task<ClienteResponseMessage> Novo(ClienteDto cliente);
    }
}
