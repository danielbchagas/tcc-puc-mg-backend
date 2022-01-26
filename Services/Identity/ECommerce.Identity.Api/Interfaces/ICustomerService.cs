using System.Threading.Tasks;
using ECommerce.Identity.Api.Models;
using Refit;

namespace ECommerce.Identity.Api.Interfaces
{
    public interface ICustomerService
    {
        [Post("/api/customers")]
        Task<IApiResponse> Create([Body]CustomerDto cliente, [Authorize("Bearer")] string accessToken);
    }
}
