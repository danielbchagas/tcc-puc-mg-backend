using ECommerce.Identity.Api.Models.Response;
using Refit;
using System.Threading.Tasks;

namespace ECommerce.Identity.Api.Interfaces
{
    public interface IViaCepRequest
    {
        [Get("/{zipCode}/json/")]
        Task<ViaCepResponse> Get([Query] string zipCode);
    }
}
