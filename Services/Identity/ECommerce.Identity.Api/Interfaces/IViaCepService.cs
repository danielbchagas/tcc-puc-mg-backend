using ECommerce.Identity.Api.Models.Request;
using System.Threading.Tasks;

namespace ECommerce.Identity.Api.Interfaces
{
    public interface IViaCepService
    {
        Task<AddressRequest> GetAddress(string zipCode);
    }
}
