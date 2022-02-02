using System.Threading.Tasks;
using ECommerce.Identity.Api.Models;
using Refit;

namespace ECommerce.Identity.Api.Interfaces
{
    public interface ICustomerService
    {
        [Post("/api/customer")]
        Task<ApiResponse<object>> Create([Body]CustomerDto customer, [Authorize("Bearer")] string accessToken);
    }
}
